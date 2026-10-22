using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;

namespace Wypozyczalnia.Tests.Architecture;

/// <summary>
/// Bramka architektury backendu (NetArchTest + refleksja, czesc dotnet test). Reguly: kontroler -> serwis -> repozytorium,
/// kontroler bez repozytorium, nazwy warstw (*Controller, *Service, *Repository), DTO tylko w przestrzeni Dto,
/// SQL tylko w repozytorium, kwoty bez typow zmiennoprzecinkowych. Sprawdzany jest kod produkcyjny (assembly Wypozyczalnia.Api).
/// Komunikat "because" mowi, co nie gra i dlaczego - trafia do raportu bledu testu razem z lista klas.
/// </summary>
public class ArchitectureTest
{
    private const string Controllers = "Wypozyczalnia.Controllers";
    private const string Services = "Wypozyczalnia.Services";
    private const string Repositories = "Wypozyczalnia.Repositories";
    private const string Dto = "Wypozyczalnia.Dto";
    private const string Model = "Wypozyczalnia.Model";

    private static readonly Assembly ProductionCode = typeof(Program).Assembly;

    /// <summary>Klasy napisane w kodzie (bez typow generowanych przez kompilator: lambdy, regex, iteratory).</summary>
    private static PredicateList WrittenTypes() => Types.InAssembly(ProductionCode)
        .That().DoNotHaveCustomAttribute(typeof(CompilerGeneratedAttribute))
        .And().AreNotNested();

    /// <summary>Kierunek zaleznosci tylko w dol: kontroler -> serwis -> repozytorium.</summary>
    [Fact(DisplayName = "warstwy: zaleznosci tylko w dol")]
    public void Warstwy()
    {
        const string because = "zaleznosci ida tylko w dol: kontroler -> serwis -> repozytorium; serwis nie zna kontrolera, repozytorium nie zna serwisu";

        AssertRule(WrittenTypes().And().ResideInNamespace(Services)
            .ShouldNot().HaveDependencyOn(Controllers).GetResult(), because);
        AssertRule(WrittenTypes().And().ResideInNamespace(Repositories)
            .ShouldNot().HaveDependencyOnAny(Controllers, Services).GetResult(), because);
    }

    /// <summary>Kontroler nie zna repozytorium: dane tylko przez serwis.</summary>
    [Fact(DisplayName = "kontrolerBezRepozytorium")]
    public void KontrolerBezRepozytorium()
    {
        AssertRule(WrittenTypes().And().ResideInNamespace(Controllers)
                .ShouldNot().HaveDependencyOn(Repositories).GetResult(),
            "kontroler nie zna repozytorium; dostep do danych tylko przez serwis");
    }

    /// <summary>Repozytorium nie zna DTO ani serwisu (DTO = granica API, nie bazy).</summary>
    [Fact(DisplayName = "repozytoriumBezDtoISerwisu")]
    public void RepozytoriumBezDtoISerwisu()
    {
        AssertRule(WrittenTypes().And().ResideInNamespace(Repositories)
                .ShouldNot().HaveDependencyOnAny(Dto, Services).GetResult(),
            "repozytorium nie zna DTO ani regul biznesowych");
    }

    /// <summary>SQL i ADO.NET (Microsoft.Data.Sqlite, System.Data) wylacznie w przestrzeni Repositories.</summary>
    [Fact(DisplayName = "sqlTylkoWRepozytorium")]
    public void SqlTylkoWRepozytorium()
    {
        AssertRule(WrittenTypes().And().DoNotResideInNamespace(Repositories)
                .ShouldNot().HaveDependencyOnAny("Microsoft.Data.Sqlite", "System.Data").GetResult(),
            "SQL wylacznie w repozytoriach; poza nimi nie ma ADO.NET ani Microsoft.Data.Sqlite");
    }

    /// <summary>Kontroler REST (dziedziczy po ControllerBase) w Controllers, z nazwa *Controller (ApiExceptionHandler to nie kontroler).</summary>
    [Fact(DisplayName = "nazwyKontrolerow")]
    public void NazwyKontrolerow()
    {
        AssertRule(WrittenTypes().And().Inherit(typeof(ControllerBase))
                .Should().HaveNameEndingWith("Controller").And().ResideInNamespace(Controllers).GetResult(),
            "kontroler REST nazywa sie *Controller i lezy w przestrzeni Controllers, zeby warstwe bylo widac po nazwie");
    }

    /// <summary>Serwis = klasa w Services poza wyjatkami biznesowymi; nazwa konczy sie na Service.</summary>
    [Fact(DisplayName = "nazwySerwisow")]
    public void NazwySerwisow()
    {
        AssertRule(WrittenTypes().And().ResideInNamespace(Services).And().AreClasses().And().DoNotInherit(typeof(Exception))
                .Should().HaveNameEndingWith("Service").GetResult(),
            "serwis nazywa sie *Service i lezy w przestrzeni Services, zeby warstwe bylo widac po nazwie");
    }

    /// <summary>
    /// Repozytorium = klasa bezposrednio w Repositories; nazwa konczy sie na Repository.
    /// Infrastruktura bazy (polaczenie, start bazy, format dat) lezy w Repositories.Sqlite i nie jest repozytorium.
    /// </summary>
    [Fact(DisplayName = "nazwyRepozytoriow")]
    public void NazwyRepozytoriow()
    {
        AssertRule(WrittenTypes().And().ResideInNamespaceMatching(@"^Wypozyczalnia\.Repositories$").And().AreClasses()
                .Should().HaveNameEndingWith("Repository").GetResult(),
            "repozytorium nazywa sie *Repository i lezy w przestrzeni Repositories, zeby warstwe bylo widac po nazwie");
    }

    /// <summary>Nazwa *Controller / *Service / *Repository zarezerwowana dla swojej warstwy.</summary>
    [Fact(DisplayName = "sufiksKontroleraTylkoWController")]
    public void SufiksKontroleraTylkoWController()
    {
        AssertRule(WrittenTypes().And().HaveNameEndingWith("Controller")
                .Should().ResideInNamespace(Controllers).GetResult(),
            "klasa z nazwa *Controller nalezy do przestrzeni Controllers - nazwa nie moze wprowadzac w blad");
    }

    [Fact(DisplayName = "sufiksSerwisuTylkoWService")]
    public void SufiksSerwisuTylkoWService()
    {
        AssertRule(WrittenTypes().And().HaveNameEndingWith("Service")
                .Should().ResideInNamespace(Services).GetResult(),
            "klasa z nazwa *Service nalezy do przestrzeni Services - nazwa nie moze wprowadzac w blad");
    }

    [Fact(DisplayName = "sufiksRepozytoriumTylkoWRepository")]
    public void SufiksRepozytoriumTylkoWRepository()
    {
        AssertRule(WrittenTypes().And().HaveNameEndingWith("Repository")
                .Should().ResideInNamespace(Repositories).GetResult(),
            "klasa z nazwa *Repository nalezy do przestrzeni Repositories - nazwa nie moze wprowadzac w blad");
    }

    /// <summary>DTO (obiekty granicy API: *Request, *Response) tylko w przestrzeni Dto.</summary>
    [Fact(DisplayName = "dtoTylkoWPakiecieDto")]
    public void DtoTylkoWPakiecieDto()
    {
        AssertRule(Types.InAssembly(ProductionCode)
                .That().DoNotHaveCustomAttribute(typeof(CompilerGeneratedAttribute)).And().AreNotNested()
                .And().HaveNameEndingWith("Request").Or().HaveNameEndingWith("Response")
                .Should().ResideInNamespace(Dto).GetResult(),
            "DTO (*Request, *Response) to granica API i lezy tylko w przestrzeni Dto, nie w modelu ani serwisie");
    }

    /// <summary>Kwoty tylko decimal: w modelu i serwisie zero pol, wlasciwosci, parametrow i wynikow double/float.</summary>
    [Fact(DisplayName = "kwotyBezDoubleIFloat")]
    public void KwotyBezDoubleIFloat()
    {
        const BindingFlags all = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                                 | BindingFlags.Static | BindingFlags.DeclaredOnly;
        var violations = new List<string>();

        IEnumerable<Type> checkedTypes = ProductionCode.GetTypes()
            .Where(type => type.Namespace is { } ns
                           && (ns == Model || ns.StartsWith(Model + ".") || ns == Services || ns.StartsWith(Services + ".")));
        foreach (Type type in checkedTypes)
        {
            foreach (FieldInfo field in type.GetFields(all).Where(field => IsFloatingPoint(field.FieldType)))
            {
                violations.Add($"{type.FullName}.{field.Name} ma typ {field.FieldType.Name}");
            }
            foreach (PropertyInfo property in type.GetProperties(all).Where(property => IsFloatingPoint(property.PropertyType)))
            {
                violations.Add($"{type.FullName}.{property.Name} ma typ {property.PropertyType.Name}");
            }
            foreach (MethodBase method in type.GetMethods(all).Cast<MethodBase>().Concat(type.GetConstructors(all)))
            {
                if (method is MethodInfo methodInfo && IsFloatingPoint(methodInfo.ReturnType))
                {
                    violations.Add($"{type.FullName}.{method.Name} zwraca {methodInfo.ReturnType.Name}");
                }
                foreach (ParameterInfo parameter in method.GetParameters().Where(parameter => IsFloatingPoint(parameter.ParameterType)))
                {
                    violations.Add($"{type.FullName}.{method.Name} przyjmuje {parameter.ParameterType.Name}");
                }
            }
        }

        Assert.True(violations.Count == 0,
            "kwoty w decimal; double/float gubi grosze. Naruszenia: " + string.Join("; ", violations));
    }

    private static bool IsFloatingPoint(Type type)
    {
        Type underlying = Nullable.GetUnderlyingType(type) ?? type;
        return underlying == typeof(double) || underlying == typeof(float);
    }

    private static void AssertRule(TestResult result, string because)
    {
        string failing = string.Join(", ", result.FailingTypeNames ?? []);
        Assert.True(result.IsSuccessful, $"{because}. Klasy niezgodne z regula: {failing}");
    }
}
