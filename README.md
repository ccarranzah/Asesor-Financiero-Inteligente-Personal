# 📊 Asesor Financiero Inteligente Personal

## 📌 Descripción
El **Asesor Financiero Inteligente Personal** es un sistema basado en conocimiento diseñado para ayudar a los usuarios a gestionar sus **ingresos, gastos, deudas e inversiones** de manera eficiente. Utiliza reglas financieras, aprendizaje basado en casos y algoritmos de optimización para proporcionar **recomendaciones personalizadas** y alertas en tiempo real.

📄 [English Version](README_EN.md)

## 🎯 Objetivo
Brindar asesoramiento financiero inteligente basado en la **situación económica personal** del usuario, ayudando a mejorar la planificación financiera y la toma de decisiones.

## ⚙️ Componentes del Sistema
- **Base de Reglas**: Define estrategias para gestión de ingresos, gastos, deudas e inversiones.
- **Base de Hechos**: Almacena datos financieros del usuario, incluyendo historial de transacciones.
- **Motor de Inferencia**: Aplica reglas y lógica financiera para generar recomendaciones.

## 🛠️ Tecnologías Utilizadas
- **Backend**: `.NET Core 9` con **C#**
- **Base de Datos**: `SQLite`
- **Lógica de Inferencia**: `NRules (First Order Logic - FOL)`

## 🚀 Funcionalidades Clave
✅ Análisis automático de ingresos y gastos  
✅ Recomendaciones personalizadas para ahorro e inversión  
✅ Alertas financieras en tiempo real  
✅ Optimización del pago de deudas  
✅ Visualización de datos financieros  

## 📌 Casos de Uso
Agregar aqui casos de uso

## 📂 Instalación y Ejecución
Si no tienes experiencia en programación, sigue estos pasos detallados para instalar y ejecutar el proyecto correctamente en tu computadora.

### 🖥️ Requisitos previos
Antes de empezar, asegúrate de tener instalado en tu computadora:
1. [**Git**](https://git-scm.com/downloads): Necesario para descargar el código del repositorio.
2. [**.NET SDK**](https://dotnet.microsoft.com/en-us/download): Necesario para ejecutar la aplicación.

Para verificar si están instalados:
- Abre la terminal o consola de comandos:
  - **Windows**: Presiona `Win + R`, escribe `cmd` y presiona `Enter`.
  - **Mac/Linux**: Abre la terminal (`Terminal` en Mac o `Ctrl + Alt + T` en Linux).
- Escribe los siguientes comandos y presiona `Enter`:
  ```bash
  git --version
  dotnet --version
  ```
  - Si ves un número de versión, significa que están instalados.
  - Si da error, instala **Git** y **.NET SDK** desde los enlaces proporcionados arriba.

### 📥 Paso 1: Descargar el proyecto
1. Abre la terminal o línea de comandos.
2. Escribe el siguiente comando para descargar el código del repositorio:
   ```bash
   git clone https://github.com/ccarranzah/Asesor-Financiero-Inteligente-Personal.git
   ```
3. Ingresa a la carpeta del proyecto:
   ```bash
   cd .\\Asesor-Financiero-Inteligente-Personal\\src\\SmartFinanceAI\\SmartFinanceAI.App
   ```

### ⚙️ Paso 2: Instalar las dependencias necesarias
Una vez dentro de la carpeta del proyecto, instala los paquetes necesarios ejecutando:
```bash
   dotnet restore
```
Esto descargará todas las herramientas que necesita el proyecto para funcionar correctamente.

### ▶️ Paso 3: Ejecutar el proyecto
Para iniciar el sistema, ejecuta:
```bash
   dotnet run
```
Después de unos segundos, el sistema estará funcionando y mostrará información en la consola.

### 🔄 Paso 4: Interactuar con el sistema
- Dependiendo de la configuración, puedes ver resultados directamente en la terminal o en una interfaz web si el proyecto la incluye.
- Sigue las instrucciones en pantalla para probar sus funcionalidades.
## 🛠️ Desarrollo y Colaboración
Si deseas contribuir, revisa el archivo **[CONTRIBUTING](CONTRIBUTING.md)** para pautas de desarrollo. Cualquier sugerencia o mejora es bienvenida 🚀.

## 📄 Licencia
Este proyecto está bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.
