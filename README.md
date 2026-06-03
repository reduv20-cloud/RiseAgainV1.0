# Rise Again

**Rise Again** este un joc de strategie text-based, realizat în C# Console, în care jucătorul administrează un regat aflat în dezvoltare. Scopul jocului este să crești populația, să gestionezi resursele, să îmbunătățești clădirile, să construiești o armată și să aperi regatul de amenințări externe.

Proiectul este construit gradual, cu accent pe învățarea conceptelor de programare orientată pe obiecte în C#.

## Concept

Jucătorul controlează un regat care evoluează de la o așezare mică la o putere militară și economică. Fiecare zi aduce producție de resurse, consum de hrană, creșterea amenințării externe și posibilitatea unor atacuri inamice.

Deciziile importante sunt făcute manual de player:

- când crește populația;
- când face upgrade la clădiri;
- când crește nivelul regatului;
- ce tipuri de soldați antrenează;
- cum își echilibrează economia civilă și armata.

## Funcționalități principale

### Sistem de zile

Jocul avansează prin acțiunea `Next Day`.

În fiecare zi:

- clădirile produc resurse;
- populația consumă rații de mâncare;
- Threat Level crește;
- poate apărea un atac inamic;
- progresul este salvat automat.

### Sistem de resurse

Regatul folosește mai multe resurse:

- `Food` — consumată zilnic de populație și folosită pentru soldați;
- `Wood` — folosit pentru clădiri și arcași;
- `Stone` — folosit în principal pentru upgrade-uri;
- `Gold` — resursă generală pentru upgrade-uri și soldați;
- `Iron` — resursă militară, folosită pentru unități armate.

### Clădiri

Fiecare clădire are nivel propriu și poate fi îmbunătățită.

Clădiri existente:

- `Farm` — produce Food;
- `Barracks` — crește limita maximă a populației;
- `Gold Mine` — produce Gold;
- `Stone Quarry` — produce Stone;
- `Lumber Mill` — produce Wood;
- `Iron Mine` — produce Iron;
- `Training Barracks` — permite antrenarea soldaților.

### Upgrade pentru clădiri

Clădirile pot fi upgradate cu resurse.

Reguli:

- până la Kingdom Level 3, clădirile pot fi upgradate liber, dacă există resurse;
- de la Kingdom Level 3 în sus, clădirile pot avea maximum `Kingdom Level + 3`;
- toate clădirile sunt luate în calcul pentru level up-ul regatului.

### Populație

Populația este separată de armată.

Reguli:

- fiecare om consumă 1 rație de Food pe zi;
- populația crește doar dacă playerul alege manual `Grow Population`;
- creșterea populației necesită resurse;
- dacă hrana nu ajunge, populația scade în funcție de rațiile lipsă;
- Game Over apare doar când populația ajunge la 0.

### Level Up Kingdom

Regatul poate crește în nivel doar manual, la alegerea playerului.

Pentru level up sunt necesare:

- un minim de populație;
- toate clădirile să fie cel puțin cu 1 level peste nivelul actual al regatului;
- un cost de resurse;
- costul crește progresiv cu 20% de la level la level.

Există și o funcție de afișare a cerințelor pentru următorul level.

### Sistem de armată

Armata este separată de populație.

Soldați disponibili:

- `Archer`;
- `Swordsman`;
- `Infantry`.

Soldații au:

- tip;
- level;
- număr de unități.

Training Barracks determină nivelul maxim al soldaților care pot fi antrenați. De exemplu, dacă `Training Barracks` este level 5, playerul poate crea soldați de level 1 până la level 5.

Costul soldaților crește progresiv în funcție de level.

### Army Power

Fiecare soldat contribuie la puterea totală a armatei.

Formula generală:

```text
Army Power = Base Power * Soldier Level * Count
