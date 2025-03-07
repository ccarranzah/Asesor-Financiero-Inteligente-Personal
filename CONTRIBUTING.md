¡Gracias por tu interés en contribuir al **Asesor Financiero Inteligente Personal**! Para mantener un desarrollo organizado y eficiente, por favor sigue estas pautas:

### 🛠️ Cómo Contribuir
1. **Fork del repositorio** en GitHub.
2. **Clona tu fork** en tu máquina local:
   ```bash
   git clone https://github.com/tu-usuario/asesor-financiero-inteligente.git
   cd asesor-financiero-inteligente
   ```
3. **Crea una rama** para tu mejora o corrección:
   ```bash
   git checkout -b mi-nueva-funcionalidad
   ```
4. **Realiza los cambios y prueba el código** antes de enviarlo.
5. **Sube los cambios a tu repositorio** y crea un **Pull Request**.

### 📂 Estructura del Código
- `src/`: Código fuente principal.
- `tests/`: Pruebas unitarias y de integración.
- `docs/`: Documentación del proyecto.

### 📌 Normas de Código
- Sigue las convenciones de `.NET` y `C#`.
- Usa nombres de variables y funciones descriptivos.
- Escribe comentarios cuando sea necesario.

### 🔀 Políticas de Ramas y Pull Requests
- **`main`**: Solo código estable y probado. No se permite commit directo.
- **`develop`**: Integración de nuevas funcionalidades antes de pasar a producción.
- **`feature/*`**: Para nuevas funcionalidades, debe ser creada desde `develop` y fusionada a `develop` mediante un Pull Request.
- **`bugfix/*`**: Para corregir errores en `develop`.
- **`hotfix/*`**: Corrección urgente en `main`, fusionándose tanto en `main` como en `develop`.
- **Antes de crear un Pull Request (PR)**:
  - Asegúrate de que la rama está actualizada con `develop`.
  - Incluye pruebas unitarias si aplica.
  - Sigue las convenciones de código.
  - Explica claramente los cambios en la descripción del PR.
  - Un PR debe ser aprobado por al menos un revisor antes de fusionarse.

### 🔍 Reporte de Errores
Si encuentras un error, por favor crea un **issue** en GitHub con la siguiente información:
- Descripción del problema.
- Pasos para reproducirlo.
- Posible solución si la conoces.

### ✅ Criterios de Aceptación
Para que tu contribución sea aceptada, debe:
- Seguir la estructura y estilo del código.
- Incluir pruebas si aplica.
- No romper funcionalidades existentes.

### 📄 Licencia
Al contribuir, aceptas que tu código será parte de un proyecto bajo la **Licencia MIT**.
