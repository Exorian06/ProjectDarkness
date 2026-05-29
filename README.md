ProjectDarkness – Technikai Ismertető & Projekt Leírás

(Félkész Projekt!!! C# programozói képzés alatt kezdtem el és nem jutottam a végére. Belefutottam a Spagettifikáció tipikus hibájába. Tanulási célzattal készült és maradt meg. Tervezem befejezni idővel!)


A ProjectDarkness egy C# és Windows Forms alapokon nyugvó, szöveges/2D hibrid RPG (Szerepjáték) prototípus. A projekt elsődleges célja a játékfejlesztésben használt alapvető szoftverarchitektúrák, az objektumorientált programozás (OOP) és a komplex állapotkezelés gyakorlati elsajátítása volt egyedi, keretrendszer nélküli környezetben.

Alkalmazott komplex technikák és programozási megoldások:


- Objektumorientált Architektúra (OOP) & Öröklődés: A játék entitásai (Játékos, Ellenségek, Tárgyak) egy szigorúan strukturált osztályhierarchiára épülnek. Megtanultam az absztrakciót és az öröklődést használni arra, hogy a különböző lények és tárgyak közös tulajdonságait (pl. életerő, statisztikák, inventory) hatékonyan kezeljem.

- Adatvezérelt (Data-Driven) Játékmenet: A karakterek statisztikái, a tárgyak tulajdonságai és a harci formulák nem "hardcoded" módon vannak beégetve, hanem külön adatstruktúrákban futnak. Ez kiváló alapja a későbbi skálázhatóságnak és játékegyensúly-állításnak (Balancing).

- Állapotgép (State Machine) Logika: A játékmenet különböző fázisai (felfedezés, harci mód, inventory kezelés, párbeszédek) közötti váltást egy központi logika vezérli, biztosítva, hogy a felhasználói felület (UI) és a háttérben futó engine állapota szinkronban maradjon.

- Algoritmikus Harcrendszer: Olyan matematikai formulák implementálása, amelyek lekezelik a sebzésszámítást, a kritikus találatokat, a védekezési értékeket (Armor/Mitigation) és az XP-szintléptetési logikákat.

Mit tanultam ebből a projektből? (Key Takeaways):


1. A decoupling (szétválasztás) fontossága: Ráébredtem, mennyire fontos a játéklogika (Backend) és a megjelenítés (UI/Windows Forms) szétválasztása. Ha a kód helyenként kusza, az pontosan azért van, mert a WinForms eseménykezelése hajlamos egybemosni a kettőt – ezt a tapasztalatot közvetlenül tudom hasznosítani modern játékmotoroknál (pl. Unity component-based architektúra), ahol már tudatosan figyelek a tiszta szétválasztásra.

2. Runtime Hibakeresés (Debugging): Megtanultam komplex, egymásra épülő változók (pl. harc közbeni életerő-változások, módosítók) követését és hibakeresését futási időben.

3. Memória- és Állapotmenedzsment: Mivel nincs automatikus játékmotor-környezet, nekem kellett gondoskodnom arról, hogy az adatok (pl. egy legyőzött szörny státusza vagy a felvett tárgyak) pontosan frissüljenek és ne okozzanak memóriaszivárgást vagy logikai zsákutcákat.
