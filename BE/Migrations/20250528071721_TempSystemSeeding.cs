using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HIPA_BE.Migrations
{
    /// <inheritdoc />
    public partial class TempSystemSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 2, 15 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 4, 13 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 4, 14 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 4, 15 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 6, 20 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 6, 21 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 6, 23 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 6, 24 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 7, 25 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 7, 26 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 7, 27 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 7, 28 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 7, 29 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 7, 30 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 7, 31 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 20 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 25 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 32 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 33 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 34 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 35 });

            migrationBuilder.DeleteData(
                table: "BodySystemOrgan",
                keyColumns: new[] { "BodySystemsID", "OrgansID" },
                keyValues: new object[] { 8, 36 });

            migrationBuilder.DeleteData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "Kardiovaskulárny systém, nazývaný obehová sústava, zabezpečuje transport krvi medzi orgánmi pomocou srdca a siete tepien, žíl a vlásočníc. Srdce pumpou distribuuje okysličenú krv do tela a vracia odkysličenú krv späť do pľúc na opätovné okysličenie. Tento nepretržitý cyklus udržiava homeostázu, reguluje telesnú teplotu a odstraňuje metabolický odpad. Zdravie kardiovaskulárneho systému je preto úzko späté so stravou, fyzickou aktivitou a životným štýlom.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: "Dýchací systém zabezpečuje výmenu plynov medzi organizmom a vonkajším prostredím prostredníctvom pľúc, priedušnice, priedušiek a dýchacích ciest. Vdychovaný vzduch dodáva organizmu potrebný kyslík, ktorý sa cez tenké steny alveol pri kapilárach dostáva do krvi, zatiaľ čo oxid uhličitý sa z krvi odstraňuje spätným výdychom. Tento proces je nevyhnutný pre bunkový metabolizmus, termoreguláciu a udržiavanie kyslosť–zásadovej rovnováhy.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 5,
                column: "Description",
                value: "Gastrointestinálny trakt, nazývaný aj tráviaci systém, je sústava orgánov od ústnej dutiny až po konečník, ktorá zabezpečuje príjem, mechanické a chemické spracovanie potravy a vstrebávanie živín. Zahŕňa pažerák, žalúdok, tenké a hrubé črevo, pečeň, pankreas a žlčník, ktoré spoločne produkujú tráviace šťavy a hormóny nevyhnutné pre štiepenie bielkovín, tukov a sacharidov. Vstrebávanie živín prebieha hlavne v tenkom čreve, zatiaľ čo hrubé črevo sa stará o absorpciu vody a formovanie stolice.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 9,
                column: "Description",
                value: "Pohybový systém tvorí kostra a kostrové svalstvo, ktoré spoločne zabezpečujú podporu tela, pohyb a ochranu vnútorných orgánov. Kosti tvoria pevný rám, ktorý slúži ako úchyt pre svaly, šľachy a väzy, zatiaľ čo kontrakcie svalov umožňujú vykonávanie rôznych pohybových úkonov. Kĺby a chrupavky zaisťujú plynulý a bezbolestný rozsah pohybu medzi jednotlivými kosťami. Udržiavanie silných svalov, dostatočnej flexibility a zdravej rovnováhy výživy je kľúčové pre správne fungovanie pohybového systému.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 10,
                column: "Description",
                value: "Centrálny nervový systém pozostáva z mozgu a miechy, ktoré spoločne spracovávajú a prenášajú informácie medzi telom a vonkajším prostredím. Mozog riadi kognitívne funkcie, ako je myslenie, pamäť a emócie, zatiaľ čo miecha zabezpečuje reflexné odpovede a prenos nervových impulzov. Integráciou zmyslových podnetov a koordináciou pohybov reguluje aj autonómne procesy, ako dýchanie či srdcová činnosť.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 11,
                column: "Description",
                value: "Periférny nervový systém tvorí sieť nervových vlákien, ganglií a plexov, ktoré spájajú centrálny nervový systém s orgánmi, svalmi a pokožkou. Funkčne sa delí na somatickú časť, riadiacu dobrovoľné pohyby a zmyslové vnímanie, a autonómnu časť, regulujúcu nevedomé procesy ako trávenie či srdcovú činnosť. Prostredníctvom senzorických a motorických vlákien prenáša signály medzi perifériou a mozgom či miechou, čo umožňuje reflexné reakcie a adaptáciu na vonkajšie podnety.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 12,
                column: "Description",
                value: "Vylučovací systém zabezpečuje odstránenie metabolických odpadových látok a udržiavanie vodno-elektrolytovej a acidobázickej rovnováhy prostredníctvom obličiek, močovodov, močového mechúra a močovej trubice. Obličky filtrujú krv, odstraňujú odpadové produkty a nadbytočnú vodu, pričom regulujú pH krvi a koncentráciu elektrolytov. Vzniknutá moč prechádza močovodmi do mechúra, kde sa uchováva až po kontrolovanom vylúčení von z tela.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 14,
                columns: new[] { "Description", "IconPath" },
                values: new object[] { "Detský vek predstavuje obdobie od narodenia až po nástup puberty, počas ktorého prebieha intenzívny fyzický, kognitívny a sociálno-emocionálny vývin. Kľúčovú úlohu tu zohráva primeraná výživa, stimulujúce a bezpečné prostredie, ako aj podpora motorických a rečových zručností. V tomto štádiu sa formujú základné návyky, sebauvedomenie a prvé medziľudské vzťahy, ktoré významne ovplyvňujú ďalší život jedinca. Kvalitná rodinná a pedagogická starostlivosť je preto nevyhnutná pre optimálny rozvoj dieťaťa.", "/images/bodySystems/child.png" });

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 15,
                columns: new[] { "Description", "IconPath" },
                values: new object[] { "Koža je najväčší orgán ľudského tela a tvorí prvú obrannú líniu proti vonkajším vplyvom, ako sú mikroorganizmy, UV žiarenie či mechanické poškodenie. Zároveň zabezpečuje termoreguláciu prostredníctvom potných žliaz a rozšírenia alebo zúženia ciev v pokožke. Vďaka bohatej sieti nervových zakončení umožňuje vnímanie dotyku, tlaku, bolesti či teploty a prispieva k zmyslovému kontaktu s okolím.", "/images/bodySystems/skin.png" });

            migrationBuilder.UpdateData(
                table: "PdfFiles",
                keyColumn: "ID",
                keyValue: 4,
                column: "BodySystemID",
                value: 9);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 3,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 6,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 9,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 13,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 16,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 19,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 23,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 24,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 27,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 28,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 29,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 30,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 31,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 32,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 33,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 34,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 35,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 36,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 37,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 38,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 39,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 40,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 41,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 42,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 43,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 44,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 45,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 46,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 47,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 48,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 49,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 50,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 51,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 52,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 53,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 54,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 55,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 56,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 57,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 58,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 59,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 60,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 61,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 62,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 63,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 64,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 65,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 66,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 67,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 68,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 69,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 70,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 71,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 72,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 73,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 74,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 75,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 76,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 77,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 78,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 79,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 80,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 81,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 82,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 83,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 84,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 85,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 86,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 87,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 88,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 89,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 90,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 91,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 92,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 93,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 94,
                column: "LastModified",
                value: new DateTime(2025, 5, 28, 7, 17, 19, 989, DateTimeKind.Utc).AddTicks(1700));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BodySystemOrgan",
                columns: new[] { "BodySystemsID", "OrgansID" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "Obehová sústava zodpovedná za cirkuláciu krvi.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: "Sústava zodpovedná za dýchanie.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 5,
                column: "Description",
                value: "Gastrointestinálny trakt.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 9,
                column: "Description",
                value: "Sústava kostí a svalov.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 10,
                column: "Description",
                value: "Mozog a miecha.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 11,
                column: "Description",
                value: "Nervy mimo mozgu a miechy.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 12,
                column: "Description",
                value: "Sústava zodpovedná za vylučovanie.");

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 14,
                columns: new[] { "Description", "IconPath" },
                values: new object[] { "Vývinové štádium.", "/images/noimage.png" });

            migrationBuilder.UpdateData(
                table: "BodySystems",
                keyColumn: "ID",
                keyValue: 15,
                columns: new[] { "Description", "IconPath" },
                values: new object[] { "Lorem ipsum", "/images/noimage.png" });

            migrationBuilder.InsertData(
                table: "BodySystems",
                columns: new[] { "ID", "Description", "IconPath", "Name" },
                values: new object[,]
                {
                    { 2, "No idea, ani Gemini nevedel", "/images/noimage.png", "Hematopoetický" },
                    { 4, "Prvá časť tráviacej sústavy.", "/images/noimage.png", "Ústna dutina" },
                    { 6, "Mužský reprodukčný systém.", "images/bodySystems/reproductive.png", "Pohlavný muž" },
                    { 7, "Ženský reprodukčný systém.", "images/bodySystems/reproductive.png", "Pohlavný žena" },
                    { 8, "Sústava žliaz s vnútornou sekréciou.", "images/bodySystems/endocrine.png", "Endokrinný" },
                    { 13, "Kostrová sústava.", "/images/noimage.png", "Kosti" }
                });

            migrationBuilder.UpdateData(
                table: "PdfFiles",
                keyColumn: "ID",
                keyValue: 4,
                column: "BodySystemID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 3,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 6,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 9,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 13,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3720));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3760));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 16,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 19,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 23,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 24,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 27,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 28,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 29,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 30,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 31,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 32,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 33,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 34,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 35,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 36,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 37,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 38,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 39,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 40,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 41,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 42,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 43,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 44,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 45,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 46,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 47,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 48,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 49,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 50,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 51,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 52,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 53,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 54,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 55,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 56,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 57,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 58,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 59,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 60,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 61,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 62,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 63,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 64,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 65,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 66,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 67,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 68,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 69,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 70,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 71,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 72,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 73,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 74,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 75,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 76,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 77,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 78,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 79,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 80,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 81,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 82,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 83,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 84,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 85,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 86,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 87,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 88,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 89,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 90,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 91,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 92,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 93,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3880));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 94,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3880));

            migrationBuilder.InsertData(
                table: "BodySystemOrgan",
                columns: new[] { "BodySystemsID", "OrgansID" },
                values: new object[,]
                {
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 15 },
                    { 4, 12 },
                    { 4, 13 },
                    { 4, 14 },
                    { 4, 15 },
                    { 6, 20 },
                    { 6, 21 },
                    { 6, 23 },
                    { 6, 24 },
                    { 7, 25 },
                    { 7, 26 },
                    { 7, 27 },
                    { 7, 28 },
                    { 7, 29 },
                    { 7, 30 },
                    { 7, 31 },
                    { 8, 4 },
                    { 8, 7 },
                    { 8, 20 },
                    { 8, 25 },
                    { 8, 32 },
                    { 8, 33 },
                    { 8, 34 },
                    { 8, 35 },
                    { 8, 36 }
                });
        }
    }
}
