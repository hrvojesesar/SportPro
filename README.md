# SportPro Backoffice

**SportPro Backoffice** je backoffice aplikacija koja na jednostavan, brz i efikasan način omogućuje vlasnicima malih i srednjih trgovina sportske opremee učinkovito upravljanje ljudima, resursima i poslovima. Glavni fokus aplikacije jest optimizacija poslovnih procesa kako bi se povećala produktivnost, transparentnost i konkurentnost tvrtki, uz osiguranje skalabilnosti i sigurnosti podataka.


&nbsp;

## Tehnologije

### Backend tehnologije

<div style="display: flex; justify-content: space-around; padding: 10px; margin: 10px; border: 1px solid #ccc; border-radius: 5px;">
  <a href="https://dotnet.microsoft.com/en-us/languages/csharp"><img src="https://upload.wikimedia.org/wikipedia/commons/f/ff/C-Sharp_Logo.svg" style="width: auto; height: 80px;"></a>
  <a href="https://dotnet.microsoft.com/en-us/learn/dotnet/what-is-dotnet"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" style="width: auto; height: 80px;"></a>
  <a href="https://learn.microsoft.com/en-us/sql/sql-server/what-is-sql-server?view=sql-server-ver16"><img src="https://upload.wikimedia.org/wikipedia/commons/9/99/Logo_M_SQL_Server.png" style="width: auto; height: 80px;"></a>
  <a href="https://azure.microsoft.com/"><img src="https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Microsoft_Azure.svg/200px-Microsoft_Azure.svg.png" style="width: auto; height: 80px;"></a>
</div>


### Frontend tehnologije

<div style="display: flex; justify-content: space-around; padding: 10px; margin: 10px; border: 1px solid #ccc; border-radius: 5px;">
  <a href="https://html.com/html5/"><img src="https://upload.wikimedia.org/wikipedia/commons/8/82/Devicon-html5-plain.svg" style="width: auto; height: 80px;"></a>
  <a href="https://getbootstrap.com/"><img src="https://upload.wikimedia.org/wikipedia/commons/b/b2/Bootstrap_logo.svg" style="width: auto; height: 80px;"></a>
  <a href="https://www.javascript.com/"><img src="https://upload.wikimedia.org/wikipedia/commons/d/d4/Javascript-shield.svg" style="width: auto; height: 80px;"></a>
</div>

&nbsp;

## Upute za pokretanje

### Preduvjeti
1. Instaliran <a href="https://visualstudio.microsoft.com/vs/community/">Visual Studio Community 2022</a>
2. Instaliran <a href="https://dotnet.microsoft.com/en-us/download/visual-studio-sdks">.NET SDK (.NET 8.0)</a>
3. Instaliran <a href="https://www.microsoft.com/en-us/sql-server/sql-server-downloads">Microsoft SQL SERVER</a>

### Upute za pokretanje
1. Klonirajte repozitorij na svoje računalo:
   ```sh
   git clone https://github.com/hrvojesesar/SportPro.git
   ```
2. Korištenjem editora poput Visual Studio 2022, otvorite projekt.
3. Unutar Package Manager Console, potrebno je pokrenuti sljedeću naredbu za pokretanje migracija na bazu podataka: <br>
   `Update-Database`
4. O gornjem izborniku, odabrati opciju: <br>
  `Build->Build Solution`
5. Pokrenuti samu aplikaciju odabirom opcije u gornjem izborniku: <br>
  `Debug->Start Debugging`

&nbsp;

##Demo aplikacije
