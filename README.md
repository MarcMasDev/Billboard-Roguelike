# PAC 3

Marc Mas Vidal  
video: https://drive.google.com/file/d/1LH6FMFky-bwZFdQ2C9liFwviObVGrH3L/view?usp=drive_link

**Índex**

[TOCM]

[TOC]

Controls
=============
Tots els controls son point and click. Arrossegar la bola blanca per jugar.

Features (PAC 3)
=============
#### Sistema complet d'inventari i botiga
El jugador pot comprar i vendre boles amb efectes únics.  
Hi ha un sistema de botiga (VendingMachineController) que ofereix boles aleatòries, amb preus i stock.  
Les boles es poden vendre amb el component **Sell**. Tot es gestiona des de **PlayerInventory** i **ItemInventory**.

#### UI dinàmica que segueix els objectes (Identificadors)
Cada bola té una UI pròpia que mostra el seu nom i estadístiques.  
Aquesta UI es manté sempre damunt la bola i segueix la seva posició gràcies al script **ScreenToWorldUI.cs** i **IdentifiersManager.cs**.  
S'agafa la informació des del ScriptableObject **BallStats** i la data guardada en ell (itemData).

#### Sistema de boles amb estadístiques i efectes únics
Les boles tenen estadístiques pròpies definides mitjançant ScriptableObjects (**BallStats**).  
Aquestes estadístiques inclouen:
- Nom i descripció.
- Comportaments únics durant el joc (afegir o restar punts en funció de la distància recorreguda, multiplicar al xocar contra una pared...)

#### Dificultat
El sistema fa ús dels ScriptableObjects **DifficultySettings** i **Difficulty**, que permeten definir dificultats personalitzades (des de Unity).

#### Sistema de forats amb comportaments propis
Els forats no només absorbeixen boles sinó que poden tenir efectes propis (sons, puntuació, canvis visuals).  
Cada forat fa servir **HoleStats** per definir el seu comportament, i la seva gestió es centralitza a **HoleManager.cs**.
Aquest comportament s'ha deixat "OnHold" per la versió de la entrega, ya que era molta feina i no he tingut temps a fer-ho (vaig ser massa optimista).

#### Comptadors i sistema de puntuació
Es mostra puntuació i informació contextual durant la partida:
- Els punts es gestionen des de **ScoreManager.cs**. Igual que les condicions de victoria i derrota.
- Hi ha UI dedicada amb efectes visuals (pop-ups, textos flotants) gestionada per **ScorePopUp.cs** i **ScoreDisplayer.cs**.
- També es mostra el contingut de l’inventari i els objectes obtinguts.

#### Menú, gestió d’escenes i logo
El joc compta amb menú principal, menú de pausa i pantalles de fi de partida.  
La gestió de les escenes es fa des de **SimpleSceneManager.cs** i **MenuManager.cs**.  
També hi ha un sistema de **OptionsMenu.cs** i gestió visual modular mitjançant **MenuGroup.cs**.
També hi ha una secció al menú principal que inclou els crèdits.
La build té un logo personalitzat.

#### Audio contextual i ambience reactiva
El joc utilitza diversos loops de música d’estil ragtime i ambient bar.  
Els sons varien segons les accions del jugador (impactes, compres, puntuació...) i es gestionen mitjançant **AudioController.cs** i el mixer **Audio.mixer**.  
També s’han afegit sons ambientals com murmuris de bar i sons de billar.
La música té un controlador que simula un concert de piano en un bar (amb aplausos de diferents intensitats).

#### Efectes visuals
Cada bola pot tenir efectes visuals personalitzats.  
Hi ha boira ambiental i lluentor a certs elements.  
Els materials segueixen una estètica píxel i paleta Fading 16.

#### Sistema modular i escalable
Tots els elements s’han fet pensant en reutilització:
- Les boles comparteixen codi però poden tenir comportaments únics via ScriptableObject.
- Els forats funcionen igual.
- La gestió del joc està separada en diferents scripts per evitar dependències excessives.

#### Codi clar i comentat
El codi està estructurat en mètodes breus i dividit en components.  
S’ha prioritzat la claredat abans que la compressió de línies.  
Moltes parts es poden reutilitzar fàcilment per altres projectes.
Si que es veritat que a vegades el codi es pot fer molt complicat ja que n'hi ha una bona cantitat i en alguns punts he escrit de més sense comentar-ho com és degut. Tot i això, hauria de ser força decent.

#### Disseny visual
El joc utilitza gràfics retro, fets amb asset packs de píxel art i una estètica coherent de bar antic.  
Tot el set-up visual ha estat personalitzat: fons, objectes decoratius i UI en estil llibreta.

#### Fonts i estètica visual
S’utilitzen tipografies antigues i manuscrites que reforcen l’ambient vintage del joc.  
El HUD s’ha fet pensant en una llibreta o quadern d’anotacions.

#### Pantalles i escenes
El projecte conté les següents escenes:
- **MainMenu.unity**: menú principal.
- **Gameplay.unity**: partida.
- **Screens.unity**: pantalles de fi de partida, victòria, derrota.
També hi ha escenes de prova i debug.

---

**Referències**

*Visuals i Gràfics*  
Pixel Wood Tileset - https://pixelkitty-art.itch.io/pixel-wood-tileset  
Pixel Art Vending Machines - https://karsiori.itch.io/pixel-art-vending-machines  
SMS Pool Asset Pack FREE - (autor no especificat, descarregat via carpeta local)  
Tiny RPG - Dragon Regalia GUI - (autor no especificat, descarregat via carpeta local)  
Paleta de colors Fading 16 - https://lospec.com/palette-list/fading-16

*UI i Tipografies*  
Complete UI Essential Pack - https://crusenho.itch.io/complete-ui-essential-pack  
Smart Duck Font - https://www.1001fonts.com/smart-duck-font.html  
First Time Writing Font - https://www.fontspace.com/first-time-writing-font-f68087  
Pixelfont (Font1) i Pixelfont2 (Font2) - autor no especificat, arxius locals amb llicència adjunta

*Audio i Música*  
Peces de piano d’Irving Berlin (1914–1933), enregistrades en viu per Nesrality (Pixabay)  
Easter Parade, Homesick, Alexander's Ragtime Band, All Alone, Always, Araby, At Peace With The World, Because I Love You, I Love a Piano, Follow The Crowd, Home Again Blues

Sons ambientals:  
- Restaurant ambience V2.WAV – DDT197 – [CC0] – https://freesound.org/s/445791/  
- Bar interior – felix.blume – [CC0] – https://freesound.org/s/684406/  
- Billiard ball clack – Za-Games – [CC0] – https://freesound.org/s/539854/  
- The Devil’s Etude – WelvynZPorterSamples – [CC0] – https://freesound.org/s/621384/  
- Altres: moxobna, snakebarney, Breviceps – [CC0]
---