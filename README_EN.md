# 📊 Personal Intelligent Financial Advisor

## 📌 Description
The **Personal Intelligent Financial Advisor** is a knowledge-based system designed to help users efficiently manage their **income, expenses, debts, and investments**. Using financial rules, case-based learning, and optimization algorithms, the system provides **personalized recommendations** and real-time financial alerts.

📄 [Versión en Español](README.md)

## 🎯 Objective
To provide intelligent financial advice based on the user's **personal economic situation**, helping improve financial planning and decision-making.

## ⚙️ System Components
- **Rule Base**: Defines strategies for managing income, expenses, debts, and investments.
- **Fact Base**: Stores user financial data, including transaction history.
- **Inference Engine**: Applies rules and financial logic to generate recommendations.

## 🛠️ Technologies Used
- **Backend**: `.NET Core 9` with **C#**
- **Database**: `SQLite`
- **Inference Logic**: `NRules (First Order Logic - FOL)`

## 🚀 Key Features
✅ Automatic analysis of income and expenses  
✅ Personalized recommendations for savings and investment  
✅ Real-time financial alerts  
✅ Debt payment optimization  
✅ Financial data visualization  

## 📌 Use Cases
Add here use cases

## 📂 Installation & Execution
If you have no programming experience, follow these detailed steps to install and run the project correctly on your computer.

### 🖥️ Prerequisites
Before starting, ensure that you have the following installed on your computer:
1. [**Git**](https://git-scm.com/downloads): Required to download the repository.
2. [**.NET SDK**](https://dotnet.microsoft.com/en-us/download): Required to run the application.

To verify installation:
- Open the terminal or command prompt:
  - **Windows**: Press `Win + R`, type `cmd`, and press `Enter`.
  - **Mac/Linux**: Open the terminal (`Terminal` on Mac or `Ctrl + Alt + T` on Linux).
- Type the following commands and press `Enter`:
  ```bash
  git --version
  dotnet --version
  ```
  - If you see a version number, they are installed correctly.
  - If an error occurs, install **Git** and **.NET SDK** using the links above.

### 📥 Step 1: Download the Project
1. Open the terminal or command prompt.
2. Type the following command to download the repository:
   ```bash
   git clone https://github.com/ccarranzah/Asesor-Financiero-Inteligente-Personal.git
   ```
3. Navigate to the project folder:
   ```bash
   cd .\Asesor-Financiero-Inteligente-Personal\src\SmartFinanceAI\SmartFinanceAI.App
   ```

### ⚙️ Step 2: Install Required Dependencies
Once inside the project folder, install the necessary packages by running:
```bash
   dotnet restore
```
This will download all required tools for the project to function correctly.

### ▶️ Step 3: Run the Project
To start the system, execute:
```bash
   dotnet run
```
After a few seconds, the system will start running and display information in the console.

### 🔄 Step 4: Interact with the System
- Depending on the configuration, you may see results directly in the terminal or through a web interface if included.
- Follow the on-screen instructions to test its functionalities.

## 🛠️ Development & Contribution
If you want to contribute, check the **[CONTRIBUTING_EN](CONTRIBUTING_EN.md)** file for development guidelines. Suggestions and improvements are always welcome 🚀.

## 📄 License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
