[English](README.md) | [Українська](README.uk.md) | [Русский](README.ru.md)

---

# ⚓ E4K Fleet Optimizer

A specialized desktop utility for **[Empire: Four Kingdoms](https://play.google.com/store/apps/details?id=air.com.goodgamestudios.empirefourkingdoms)**. This tool completely eliminates the need for manual math by automatically calculating the most efficient ship combinations for your fleet slots, ensuring maximum capacity and optimized stats.

## ✨ Features
* **Automated Fleet Calculation:** Instantly calculates the best ship distribution to achieve maximum profit and efficiency.
* **Smart Slot Optimization:** Accurately places ships into available slots based on their individual statistics and carrying capacity.
* **Modern Interface:** A sleek, fully optimized UI featuring both **Dark and Light themes**, built to suit your preference.
* **Autonomous Processing:** Fast, local data processing that handles all complex calculations directly on your machine.

## 🏗 Architecture & Patterns
The project is built with clean code principles, ensuring high maintainability and scalability:
* **MVVM Pattern:** Leverages `CommunityToolkit.Mvvm` for strict separation between the UI (Views) and business logic (ViewModels).
* **Dependency Injection (DI):** Utilizes `Microsoft.Extensions.DependencyInjection` for loose coupling, registering services, data providers, and ViewModels via a centralized IoC container.
* **Domain-Driven Design (DDD) Elements:** The architecture is split into decoupled layers (`Core` and `WPF`). Business logic and domain models (`OptimizationResult`, immutable `record` entities) are completely isolated from UI and data access implementations.
* **Provider Pattern:** Data retrieval is abstracted via interfaces (`IShipDataProvider`, `IShipReferenceProvider`), allowing seamless switching between JSON, CSV, or future database implementations.

## 🗺️ Roadmap & Future Plans
The project is actively evolving. Here are the planned features for upcoming releases:
* **Live Map Tracking:** Integration of a background network analyzer to automatically detect and log coordinates for level 70-80 forts and free islands.
* **Multi-Language Support (Localization):** Full interface translation into multiple languages (English, Ukrainian, Russian) to support a wider global player base.
* **Web-Architecture Migration:** Transitioning the core optimization engine from a local desktop app to a full-stack web application with an independent API backend and frontend UI.

## 📸 Screenshots

1. **Budget Optimization Setup:** 
   ![Budget Optimization Setup](assets/budget-optimization-setup.png)

2. **Optimization Results:** 
   ![Optimization Results](assets/budget-optimization-results.png)

3. **Target Achievement:** 
   ![Target Achievement](assets/target-achievement-results.png)

4. **Ship Reference Data:** 
   ![Ship Reference Data](assets/ship-reference-data.png)

## 🛠 Tech Stack & Environment

**Core Framework:**
* **Language:** C#
* **Target Framework:** .NET 8.0
* **Target OS:** Windows (Minimum supported version: Windows 7.0)
* **UI Framework:** WPF (Windows Presentation Foundation)

**NuGet Dependencies:**
* `CommunityToolkit.Mvvm` (v8.4.2)
* `Microsoft.Extensions.DependencyInjection`

## 🚀 Installation & Usage

**For Players:**
1. Navigate to the [Releases](../../releases/latest) page.
2. Download the latest `E4KFleetOptimizer_Setup.exe` file.
3. Run the installer and launch the application. 
   *(Note: The application requires the [.NET 8.0 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0). The installer should handle dependencies, but you can download it manually if prompted).*

**For Developers:**
1. Clone the repository: 
   ```bash
   git clone https://github.com/Tokar08/E4KFleetOptimizer.git
   ```
2. Open `E4KFleetOptimizer.sln` in Visual Studio 2022 or newer.
3. Visual Studio will automatically restore the required NuGet packages. If using the CLI, run:
   ```bash
   dotnet restore
   ```
   
4. Build and run the project in `Release` or `Debug` mode.

## ⚖️ Legal Disclaimer
**E4K Fleet Optimizer** is an unofficial, community-driven tool and is not affiliated with, endorsed, sponsored, or specifically approved by the game's creators. 

All copyrights, trademarks, and in-game assets related to **Empire: Four Kingdoms** are the sole property of [Goodgame Studios](https://goodgamestudios.com/) and [Stillfront Group](https://www.stillfront.com/en/). This software is provided for educational and utility purposes only.
