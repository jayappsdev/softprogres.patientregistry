# Správa pacientov
Úloha na vypracovanie pre záujemcov o pozíciu junior C# .NET vývojára

## Požadované nástroje

Na vypracovanie tejto úlohy bude potrebné mať nasledujúce nástroje:
- Účet na [github.com](https://github.com/)
- Nainštalovaný [git-scm](https://git-scm.com/)
- Nainštalovaný [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download)
- Nainštalované ľubovoľné vývojové prostredie pre C# .NET:
  - [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)
  - [JetBrains Rider](https://www.jetbrains.com/rider/) (platené)
  - [Visual Studio Code](https://code.visualstudio.com/) nastavené pre [vývoj .NET](https://code.visualstudio.com/docs/languages/dotnet)
  - prípadne ľubovoľný textový editor podľa vašich preferencií

## Podmienky vypracovania zadania

Zadanie musíte vypracovať samostatne bez cudzej pomoci.

Pri vypracovaní môžete používať ľubovoľné dokumentácie, návody, videá, stackoverflow, google, a pod.

Myslite však na to, že všetko, čo spravíte, si budete musieť vedieť aj obhájiť.

Čas potrebný na vypracovanie zadania sa nijak nehodnotí.

## Popis solution a práca s git

Tento solution reprezentuje veľmi jednoduchú správu užívateľov (pacientov) a základné operácie nad nimi 
(zobrazovanie, vytváranie, zmenu údajov a mazanie).

Solution sa skladá z 2 projektov:
- Projekt `SoftProgres.PatientRegistry.Api` reprezentuje backend v .NET 8.0 s použitím [ServiceStack frameworku](https://servicestack.net/) 
(niečo, s čím sa u nás bežne stretnete). V tomto projekte sa pracuje s databázou, obsahuje biznis logiku 
a pre používateľov vystavuje rozhranie v podobe webových služieb (API).
- Projekt `SoftProgres.PatientRegistry.Desktop` reprezentuje frontend v .NET 8.0 s použitím [WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/?view=netdesktop-8.0). 
Pre štylizáciu komponentov a pár nástrojov na zjednodušenie je použitá open-source knižnica [WPF-UI](https://github.com/lepoco/wpfui).

Vašou úlohou bude vytvoriť kópiu repozitára https://github.com/softprogres/softprogres.patientregistry vo vašom github
účte (projekt nevytvárajte ako `fork` repozitára, nakoľko bude verejne viditeľný).

Vytvorte si prázdny repozitár `softprogres.patientregistry` pod vašim github účtom a pridajte do neho všetky súbory.
Spravte `commit` a `push` projektu bez zmien ako prvý commit.

Zmeny budete vykonávať do branchov, ktoré si vytvoríte:
- zmeny v projekte `SoftProgres.PatientRegistry.Api` budú commitnuté do branche `backend`.
- zmeny v projekte `SoftProgres.PatientRegistry.Desktop` budú commitnuté do branche `frontend`.

Po dokončení všetkých úloh urobíte merge branchov `backend` a `frontend` do branche `main` cez `pull request`.
Branche po mergi nemažte, commity zachovajte (nerobte squash).

Či budete používať na prácu s git-om CLI alebo nejakú GUI aplikáciu, je na vás.

## Spustenie solution

Pre správnu funkčnosť je potrebné mať spustené oba projekty. Projekty sú nastavené tak, aby server bežal na localhoste
na porte 5001. V prípade, že vám to nevyhovuje, aktualizujte port v API projekte v `Properties/launchSettings.json`
a v Desktop projekte v `appsettings.json`.

Databáza pacientov je reprezentovaná súborovou SQLite databázou v projekte `SoftProgres.PatientRegistry.Api`. Nájdete
ju v ceste `App_Data/app.db`. Môžete ju v prípade potreby zmazať a vygeneruje sa nanovo so základnou sadou pacientov.

## Úlohy v projekte SoftProgres.PatientRegistry.Api

V tomto projekte bude vašou úlohou implementovať jednoduchý algoritmus na validáciu rodného čísla a pomocné funkcie
pre získavanie dodatočných údajov z rodného čísla.

1. V súbore `Validators/BirthNumberValidator.cs` implementuje validáciu rodného čísla.
   - Za validný vstupný formát sa považuje reťazec reprezentujúci rodné číslo s lomkou aj bez lomky
   - Validácia musí spĺňať pravidlá definované v [zákone 301/1995 Z. z. o rodnom čísle § 2](https://www.slov-lex.sk/pravne-predpisy/SK/ZZ/1995/301/#paragraf-2)
   - V súbore `ServiceInterface/PatientService.cs` zavolajte funkciu validácie (validátor je v premennej
   `_birthNumberValidator`) pri vytváraní pacienta (metóda `Post`) a aktualizácií údajov o pacientovi (metóda `Put`) 
   na mieste, kde si myslíte, že je to najlepšie.
2. V súbore `Helpers/BirthNumberHelper.cs` implementujete funkcionalitu na získavanie dátumu narodenia, veku 
a pohlavia osoby z rodného čísla.
   - Za validný vstupný formát sa považuje reťazec reprezentujúci rodné číslo s lomkou aj bez lomky
   - Predpokladajte, že rodné číslo na vstupe je už validné podľa [zákona 301/1995 Z. z. o rodnom čísle § 2](https://www.slov-lex.sk/pravne-predpisy/SK/ZZ/1995/301/#paragraf-2).
   - V súbore `Database/DataProvider.cs` zavolajte pomocné funkcie (helper je v premennej `_birthNumberHelper`) 
   pri vytváraní pacienta (metóda `CreatePatientAsync`) a aktualizácii údajov o pacientovi (metóda `UpdatePatientAsync`)
   na mieste, kde si myslíte, že je to najlepšie.

## Úlohy v projekte SoftProgres.PatientRegistry.Desktop

V tomto projekte budete musieť vytvoriť converter, pracovať s WPF bindingami, systémovými dialógmi a súborovým systémom
a vytvoriť jednoduchý formulár.

1. V súbore `Converters/SexEnumToStringConverter` vytvorte IValueConverter, ktorý na vstupe dostane enum reprezentujúci
pohlavie pacienta (enum sa nachádza v súbore `Models/Sex.cs` a vráti reťazec v tvare "muž", "žena", alebo "neznáme".
2. V súbore `Views/Pages/PatientRegistryPage.xaml` musíte vyriešiť zobrazovanie hodnôt v 3 stĺpcoch v DataGrid-e:
   - Stĺpec `Pohlavie` musí zobraziť hodnotu s použitím convertera vytvoreného v 1. úlohe
   - Stĺpec `Dátum narodenia` musí zobraziť hodnotu, ktorá bude v bindingu naformátovaná do tvaru `dd.mm.rrrr`
   - Stĺpec `Adresa` musí zobraziť v sebe kombináciu kompletnej adresy zo 4 premenných v tvare 
   "Ulica, PSČ Mesto, Štát", čiže napr. "Nálepková 123/13, 921 01 Piešťany, Slovensko".
3. V súbore `ViewModels/PatientRegistryViewModel.cs` implementuje vo funkcii `DeletePatientAsync` volanie systémového
`MessageBox`, ktorý vyžiada užívateľské potvrdenie zmazania pacienta.
4. V súbore `ViewModels/PatientRegistryViewModel.cs` implementuje vo funkcii `ExportToCsvAsync` funkcionalitu exportu 
dát aktuálneho zoznamu pacientov do CSV súboru. Užívateľovi musíte umožniť výber výstupného súboru cez systémový dialóg.
5. V súbore `Views/Pages/EditPatientPage.xaml` vytvorte jednoduchý formulár, ktorý bude aktualizovať pacientské údaje 
nachádzajúce sa v premennej ViewModel-u `CurrentPatient`. Vstupné hodnoty nie je potrebné validovať.

## Ukončenie práce

Vypracovanie zadania nemusí byť na 100%, urobte toľko, koľko zvládnete.

V prípade, že si nebudete niečím istý, neváhajte ma kontaktovať na e-mailovej adrese `lukas.rendvansky@softprogres.sk`.

Link na váš repozitár s vypracovaným zadaním po dokončení odošlite na e-mail `hr@softprogres.sk` do 20.10.2024 (nedeľa)
do polnoci. Uistite sa, že repozitár je verejný.