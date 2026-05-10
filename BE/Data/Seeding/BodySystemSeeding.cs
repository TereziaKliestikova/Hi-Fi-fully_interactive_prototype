using HIPA_BE.Models.BodySystemModels;
using Microsoft.EntityFrameworkCore;

namespace HIPA_BE.Data.Seeding
{
    public class BodySystemSeeding
    {
        public static List<BodySystem> GetModels()
        {
            return new List<BodySystem>
            {
                // new BodySystem
                // {
                //     ID = 2,
                //     Name = "Digestive System",
                //     Description = "Lorem impsum random description",
                //     IconPath = "images/bodySystems/digestive.png",
                // },
                // new BodySystem
                // {
                //     ID = 4,
                //     Name = "Integumentary System",
                //     Description = "Lorem impsum random description",
                //     IconPath = "images/bodySystems/urinary.png",
                // },
                // new BodySystem
                // {
                //     ID = 5,
                //     Name = "Lymphatic System",
                //     Description = "Lorem impsum random description",
                //     IconPath = "images/bodySystems/lymphatic.png",
                // },
                new BodySystem
                {
                    ID = 1,
                    Name = "Kardiovaskulárny",
                    Description = "Kardiovaskulárny systém, nazývaný obehová sústava, zabezpečuje transport krvi medzi orgánmi pomocou srdca a siete tepien, žíl a vlásočníc. Srdce pumpou distribuuje okysličenú krv do tela a vracia odkysličenú krv späť do pľúc na opätovné okysličenie. Tento nepretržitý cyklus udržiava homeostázu, reguluje telesnú teplotu a odstraňuje metabolický odpad. Zdravie kardiovaskulárneho systému je preto úzko späté so stravou, fyzickou aktivitou a životným štýlom.",
                    IconPath = "images/bodySystems/cardiovascular.png",
                },
                // new BodySystem
                // {
                //     ID = 2,
                //     Name = "Hematopoetický",
                //     Description = "No idea, ani Gemini nevedel",
                //     IconPath = "/images/noimage.png",
                // },
                new BodySystem
                {
                    ID = 3,
                    Name = "Dýchací",
                    Description = "Dýchací systém zabezpečuje výmenu plynov medzi organizmom a vonkajším prostredím prostredníctvom pľúc, priedušnice, priedušiek a dýchacích ciest. Vdychovaný vzduch dodáva organizmu potrebný kyslík, ktorý sa cez tenké steny alveol pri kapilárach dostáva do krvi, zatiaľ čo oxid uhličitý sa z krvi odstraňuje spätným výdychom. Tento proces je nevyhnutný pre bunkový metabolizmus, termoreguláciu a udržiavanie kyslosť–zásadovej rovnováhy.",
                    IconPath = "images/bodySystems/respiratory.png",
                },
                // new BodySystem
                // {
                //     ID = 4,
                //     Name = "Ústna dutina",
                //     Description = "Prvá časť tráviacej sústavy.",
                //     IconPath = "/images/noimage.png",
                // },
                new BodySystem
                {
                    ID = 5,
                    Name = "GIT",
                    Description = "Gastrointestinálny trakt, nazývaný aj tráviaci systém, je sústava orgánov od ústnej dutiny až po konečník, ktorá zabezpečuje príjem, mechanické a chemické spracovanie potravy a vstrebávanie živín. Zahŕňa pažerák, žalúdok, tenké a hrubé črevo, pečeň, pankreas a žlčník, ktoré spoločne produkujú tráviace šťavy a hormóny nevyhnutné pre štiepenie bielkovín, tukov a sacharidov. Vstrebávanie živín prebieha hlavne v tenkom čreve, zatiaľ čo hrubé črevo sa stará o absorpciu vody a formovanie stolice.",
                    IconPath = "images/bodySystems/digestive.png",
                },
                // new BodySystem
                // {
                //     ID = 6,
                //     Name = "Pohlavný muž",
                //     Description = "Pohlavný (reprodukčný) systém muža zabezpečuje tvorbu, dozrievanie a transport spermií a pohlavných hormónov. Hlavnými orgánmi sú semenníky, kde vznikajú spermie a testosterón, nadsemenníky, ktoré ich skladujú a vyživujú, a semenovody vedúce spermie do močovej trubice. Prídavné žľazy – semenné váčky, prostata a bulbo-uretrálne žľazy – pridávajú k spermiám živiny a ochranné látky nevyhnutné pre ich prežitie v ženskom pohlavnom trakte. Zdravé fungovanie systému závisí od hormonálnej rovnováhy, životného štýlu a pravidelnej lekárskej starostlivosti.",
                //     IconPath = "images/bodySystems/reproductive.png",
                // },
                // new BodySystem
                // {
                //     ID = 7,
                //     Name = "Pohlavný žena",
                //     Description = "Pohlavný (reprodukčný) systém ženy zabezpečuje produkciu vajíčok, vylučovanie pohlavných hormónov a vytvorenie prostredia pre oplodnenie a vývoj plodu. Hlavné orgány zahŕňajú vaječníky, vajcovody, maternicu a pošvu, pričom prídavné žľazy a hormóny riadia menštruačný cyklus a prípravu maternice na tehotenstvo. Počas menštruačného cyklu sa z vaječníka uvoľňuje vajíčko cez vajcovod, kde môže dôjsť k oplodneniu, a následne sa pripravuje nástup možného tehotenstva v maternici. Optimálne fungovanie podporuje hormonálna rovnováha, vyvážená strava, pravidelná fyzická aktivita a pravidelné gynekologické prehliadky.",
                //     IconPath = "images/bodySystems/reproductive.png",
                // },
                // new BodySystem
                // {
                //     ID = 8,
                //     Name = "Endokrinný",
                //     Description = "Sústava žliaz s vnútornou sekréciou.",
                //     IconPath = "images/bodySystems/endocrine.png",
                // },
                new BodySystem
                {
                    ID = 9,
                    Name = "Pohybový",
                    Description = "Pohybový systém tvorí kostra a kostrové svalstvo, ktoré spoločne zabezpečujú podporu tela, pohyb a ochranu vnútorných orgánov. Kosti tvoria pevný rám, ktorý slúži ako úchyt pre svaly, šľachy a väzy, zatiaľ čo kontrakcie svalov umožňujú vykonávanie rôznych pohybových úkonov. Kĺby a chrupavky zaisťujú plynulý a bezbolestný rozsah pohybu medzi jednotlivými kosťami. Udržiavanie silných svalov, dostatočnej flexibility a zdravej rovnováhy výživy je kľúčové pre správne fungovanie pohybového systému.",
                    IconPath = "images/bodySystems/muscular.png",
                },
                new BodySystem
                {
                    ID = 10,
                    Name = "Centrálny nervový",
                    Description = "Centrálny nervový systém pozostáva z mozgu a miechy, ktoré spoločne spracovávajú a prenášajú informácie medzi telom a vonkajším prostredím. Mozog riadi kognitívne funkcie, ako je myslenie, pamäť a emócie, zatiaľ čo miecha zabezpečuje reflexné odpovede a prenos nervových impulzov. Integráciou zmyslových podnetov a koordináciou pohybov reguluje aj autonómne procesy, ako dýchanie či srdcová činnosť.",
                    IconPath = "images/bodySystems/nervous.png",
                },
                new BodySystem
                {
                    ID = 11,
                    Name = "Periferný nervový",
                    Description = "Periférny nervový systém tvorí sieť nervových vlákien, ganglií a plexov, ktoré spájajú centrálny nervový systém s orgánmi, svalmi a pokožkou. Funkčne sa delí na somatickú časť, riadiacu dobrovoľné pohyby a zmyslové vnímanie, a autonómnu časť, regulujúcu nevedomé procesy ako trávenie či srdcovú činnosť. Prostredníctvom senzorických a motorických vlákien prenáša signály medzi perifériou a mozgom či miechou, čo umožňuje reflexné reakcie a adaptáciu na vonkajšie podnety.",
                    IconPath = "images/bodySystems/nervous.png",
                },
                new BodySystem
                {
                    ID = 12,
                    Name = "Vylučovací",
                    Description = "Vylučovací systém zabezpečuje odstránenie metabolických odpadových látok a udržiavanie vodno-elektrolytovej a acidobázickej rovnováhy prostredníctvom obličiek, močovodov, močového mechúra a močovej trubice. Obličky filtrujú krv, odstraňujú odpadové produkty a nadbytočnú vodu, pričom regulujú pH krvi a koncentráciu elektrolytov. Vzniknutá moč prechádza močovodmi do mechúra, kde sa uchováva až po kontrolovanom vylúčení von z tela.",
                    IconPath = "images/bodySystems/urinary.png",
                },
                // new BodySystem
                // {
                //     ID = 13,
                //     Name = "Kosti",
                //     Description = "Kostrová sústava.",
                //     IconPath = "/images/noimage.png",
                // },
                new BodySystem
                {
                    ID = 14,
                    Name = "Detský vek",
                    Description = "Detský vek predstavuje obdobie od narodenia až po nástup puberty, počas ktorého prebieha intenzívny fyzický, kognitívny a sociálno-emocionálny vývin. Kľúčovú úlohu tu zohráva primeraná výživa, stimulujúce a bezpečné prostredie, ako aj podpora motorických a rečových zručností. V tomto štádiu sa formujú základné návyky, sebauvedomenie a prvé medziľudské vzťahy, ktoré významne ovplyvňujú ďalší život jedinca. Kvalitná rodinná a pedagogická starostlivosť je preto nevyhnutná pre optimálny rozvoj dieťaťa.",
                    IconPath = "/images/bodySystems/child.png",
                },
                new BodySystem
                {
                    ID = 15,
                    Name = "Koža",
                    Description = "Koža je najväčší orgán ľudského tela a tvorí prvú obrannú líniu proti vonkajším vplyvom, ako sú mikroorganizmy, UV žiarenie či mechanické poškodenie. Zároveň zabezpečuje termoreguláciu prostredníctvom potných žliaz a rozšírenia alebo zúženia ciev v pokožke. Vďaka bohatej sieti nervových zakončení umožňuje vnímanie dotyku, tlaku, bolesti či teploty a prispieva k zmyslovému kontaktu s okolím.",
                    IconPath = "/images/bodySystems/skin.png",
                },
            };
        }
    }
}
