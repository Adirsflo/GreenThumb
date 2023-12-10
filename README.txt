Reflekterande Projektanalys: GreenThumb 

Inledning:
Under mitt GreenThumb-projekt har fokus legat på design och implementering av en applikation för
trädgårdsentusiaster. Målet var att skapa en användarvänlig plattform där användaren kan hantera sina
växter och trädgårdar med hjälp av relationsdatabaser och Entity Framework Core. 

Projektöversikt:
GreenThumb-applikationen möjliggör sökning och hantering av växter, tillägg och borttagning av växter från
användarens trädgård, samt detaljerad visning av varje växts vårdinstruktioner. Navigeringen mellan olika
fönster, inklusiver SignIn-, Register-, Plant-, AddPlant-, PlantDetails och MyGardenWindow, ger användaren
en intuitiv och strukturerad upplevelse.  

Arkitektur & Databasdesign:
Projektet inleddes med noggrann ER-modellering (code-first) och utveckling av en stabil databasdesign.
Beslutet att skapa en junction table (GardenPlants) för att hantera many-to-many-relationen mellan
trädgård och växter stärkte inte bara designen utan möjliggjorde också en effektiv hantering av tillagt
datum för varje växt i användarens trädgård. 

Tekniska Lösningar & Färdigheter:
- Generiska  repository: Implementeringen av generiska repository möjliggjorde en enhetlig och
återanvändbar kodstruktur för alla entiteter, vilket vidare underlättade utvecklignsprocessen och bidrog
till ökat skalbarhet.
- Kryptering och Säkerhet: Införandet av en krypteringsnyckel och medföljande hantering genom KeyManager
stärkte applikationens säkerhet och introducerade grundläggande principer för kryptering. 
- Many-to-Many-relation och Databashanterare: Implementeringen av en many-to-many-relation mellan trädgård
och växter fördjupade förståelsen för relationsdatabaser och databashanterare, speciellt Entity Framework
Core. 

Utmaningar & Lärdomar:
Utmaningarna inkluderade att balansera design och prestanda samt att integrera olika databastyper som SQL
och SSMS. Kontinuerlig mätning och analys av prestanda gav insikter om när och hur man kan optimera koden
för att förbättra responsiviteten. 

Sammanfattning och Framtid som Utvecklare:
GreenThumb-projektet har inte bara ökat tekniska färdigheter utan också utvecklat förmågan att fatta
välgrundade designbeslut för att skapa en robust och användarvänlig applikation. Framöver ser jag
möjligheter att förbättra användarupplevelsen genom att implementera mer avancerade sökfunktioner och
grafiska visualiseringar av trädgårdsdata. Projektet har varit en lärorik resa inom databasutveckling och
bidragit till en djupare förståelse för designprinciper och tekniska lösningar.  