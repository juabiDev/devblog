# DevBlog - Guía de Estilos y Desarrollo

## 🎨 Paleta de Colores

### Colores Principales
\`\`\`css
/* Light Mode */
--primary: 220 14% 11%;           /* Negro azulado para texto principal */
--primary-foreground: 210 40% 98%; /* Blanco para texto sobre primary */
--secondary: 210 40% 96%;         /* Gris muy claro para fondos secundarios */
--secondary-foreground: 222.2 84% 4.9%; /* Negro para texto sobre secondary */

/* Dark Mode */
--primary: 210 40% 98%;           /* Blanco para texto principal */
--primary-foreground: 222.2 84% 4.9%; /* Negro para texto sobre primary */
--secondary: 217.2 32.6% 17.5%;  /* Gris oscuro para fondos secundarios */
--secondary-foreground: 210 40% 98%; /* Blanco para texto sobre secondary */
\`\`\`

### Colores de Categorías
\`\`\`css
/* UI/UX - Azul */
.category-ui-ux {
  @apply bg-blue-100 text-blue-700 dark:bg-blue-900/30 dark:text-blue-400;
}

/* Java - Naranja */
.category-java {
  @apply bg-orange-100 text-orange-700 dark:bg-orange-900/30 dark:text-orange-400;
}

/* .NET - Púrpura */
.category-dotnet {
  @apply bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400;
}

/* Spring Boot - Verde */
.category-spring {
  @apply bg-green-100 text-green-700 dark:bg-green-900/30 dark:text-green-400;
}

/* Databases - Rojo */
.category-databases {
  @apply bg-red-100 text-red-700 dark:bg-red-900/30 dark:text-red-400;
}
\`\`\`

## 📝 Tipografía

### Jerarquía de Títulos
\`\`\`css
/* Título Principal (Hero) */
.title-hero {
  @apply text-4xl md:text-5xl font-bold tracking-tight;
}

/* Título de Sección */
.title-section {
  @apply text-3xl font-bold;
}

/* Título de Artículo */
.title-article {
  @apply text-2xl font-bold;
}

/* Título de Card */
.title-card {
  @apply text-lg font-semibold;
}

/* Subtítulo */
.subtitle {
  @apply text-xl text-muted-foreground;
}
\`\`\`

### Texto Corporal
\`\`\`css
/* Texto principal */
.text-body {
  @apply text-base leading-relaxed;
}

/* Texto pequeño */
.text-small {
  @apply text-sm;
}

/* Texto muy pequeño */
.text-xs-custom {
  @apply text-xs;
}

/* Texto muted */
.text-muted {
  @apply text-muted-foreground;
}
\`\`\`

## 📏 Espaciado

### Sistema de Espaciado
\`\`\`css
/* Espaciado entre secciones principales */
.section-spacing {
  @apply mt-16;
}

/* Espaciado entre elementos */
.element-spacing {
  @apply mb-8;
}

/* Espaciado pequeño */
.small-spacing {
  @apply mb-4;
}

/* Espaciado muy pequeño */
.xs-spacing {
  @apply mb-2;
}
\`\`\`

### Contenedores
\`\`\`css
/* Contenedor principal */
.container-main {
  @apply container mx-auto px-4;
}

/* Contenedor de contenido */
.container-content {
  @apply max-w-3xl mx-auto px-4;
}

/* Contenedor ancho */
.container-wide {
  @apply max-w-6xl mx-auto px-4;
}
\`\`\`

## 🎯 Componentes Reutilizables

### Botones
\`\`\`css
/* Botón primario */
.btn-primary {
  @apply bg-primary text-primary-foreground hover:bg-primary/90;
}

/* Botón secundario */
.btn-secondary {
  @apply bg-secondary text-secondary-foreground hover:bg-secondary/80;
}

/* Botón outline */
.btn-outline {
  @apply border border-input bg-background hover:bg-accent hover:text-accent-foreground;
}

/* Botón ghost */
.btn-ghost {
  @apply hover:bg-accent hover:text-accent-foreground;
}
\`\`\`

### Cards
\`\`\`css
/* Card básica */
.card-base {
  @apply border rounded-lg overflow-hidden transition-all hover:shadow-md;
}

/* Card de post */
.card-post {
  @apply border rounded-lg overflow-hidden transition-all group-hover:shadow-md;
}

/* Card de categoría */
.card-category {
  @apply border rounded-lg p-6 transition-all hover:shadow-md group;
}
\`\`\`

### Badges/Tags
\`\`\`css
/* Badge básico */
.badge-base {
  @apply text-xs font-medium px-2 py-1 rounded-full;
}

/* Badge de categoría */
.badge-category {
  @apply text-xs font-medium px-2 py-1 rounded-full bg-primary/10 text-primary;
}
\`\`\`

## 📱 Responsive Design

### Breakpoints
\`\`\`css
/* Mobile First Approach */
/* xs: 0px - 639px (móviles) */
/* sm: 640px - 767px (móviles grandes) */
/* md: 768px - 1023px (tablets) */
/* lg: 1024px - 1279px (laptops) */
/* xl: 1280px - 1535px (desktops) */
/* 2xl: 1536px+ (pantallas grandes) */
\`\`\`

### Grids Responsivos
\`\`\`css
/* Grid de posts */
.grid-posts {
  @apply grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6;
}

/* Grid de categorías */
.grid-categories {
  @apply grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-4;
}

/* Grid de featured posts */
.grid-featured {
  @apply grid grid-cols-1 lg:grid-cols-2 gap-8;
}
\`\`\`

## 🎭 Animaciones y Transiciones

### Transiciones Estándar
\`\`\`css
/* Transición básica */
.transition-base {
  @apply transition-all duration-200 ease-in-out;
}

/* Transición de hover */
.transition-hover {
  @apply transition-colors duration-200;
}

/* Transición de transform */
.transition-transform {
  @apply transition-transform duration-300 ease-in-out;
}
\`\`\`

### Efectos de Hover
\`\`\`css
/* Hover en cards */
.hover-card {
  @apply hover:shadow-md transition-all;
}

/* Hover en imágenes */
.hover-image {
  @apply transition-transform group-hover:scale-105 duration-300;
}

/* Hover en texto */
.hover-text {
  @apply hover:text-primary transition-colors;
}
\`\`\`

## 🖼️ Imágenes y Media

### Aspectos de Imagen
\`\`\`css
/* Imagen de hero */
.aspect-hero {
  @apply aspect-[4/3];
}

/* Imagen de post */
.aspect-post {
  @apply aspect-video;
}

/* Imagen cuadrada */
.aspect-square {
  @apply aspect-square;
}

/* Avatar */
.avatar-size {
  @apply w-8 h-8 rounded-full;
}
\`\`\`

### Placeholders
\`\`\`css
/* Para imágenes placeholder, usar siempre: */
/placeholder.svg?height={height}&width={width}&text={text}
\`\`\`

## 🎨 Modo Oscuro

### Implementación
\`\`\`css
/* Usar siempre las clases dark: para modo oscuro */
.dark-compatible {
  @apply bg-background text-foreground;
  @apply dark:bg-background dark:text-foreground;
}

/* Para elementos con fondo */
.dark-bg {
  @apply bg-muted/50 dark:bg-muted/50;
}
\`\`\`

## 🚀 Performance

### Mejores Prácticas

1. **Imágenes**
   - Usar siempre `alt` descriptivo
   - Implementar lazy loading con `loading="lazy"`
   - Usar placeholder mientras cargan

2. **CSS**
   - Usar clases de Tailwind en lugar de CSS custom
   - Evitar `!important`
   - Usar variables CSS para valores repetitivos

3. **JavaScript**
   - Usar `"use client"` solo cuando sea necesario
   - Implementar Server Components por defecto
   - Lazy load componentes pesados

## 📋 Checklist de Desarrollo

### Antes de crear un componente:
- [ ] ¿Es reutilizable?
- [ ] ¿Sigue la paleta de colores?
- [ ] ¿Es responsive?
- [ ] ¿Funciona en modo oscuro?
- [ ] ¿Tiene estados de hover/focus?
- [ ] ¿Es accesible?

### Antes de hacer commit:
- [ ] Colores consistentes
- [ ] Espaciado correcto
- [ ] Responsive en todos los breakpoints
- [ ] Modo oscuro funcional
- [ ] Performance optimizada
- [ ] Accesibilidad implementada

## 🎯 Patrones de Diseño

### Layout Principal
\`\`\`tsx
<div className="min-h-screen flex flex-col">
  <Header />
  <main className="flex-1">
    {/* Contenido */}
  </main>
  <Footer />
</div>
\`\`\`

### Sección Estándar
\`\`\`tsx
<section className="mt-16">
  <div className="container mx-auto px-4">
    <h2 className="text-3xl font-bold mb-8">Título</h2>
    {/* Contenido */}
  </div>
</section>
\`\`\`

### Card de Post
\`\`\`tsx
<Link href="/post/slug" className="group">
  <div className="border rounded-lg overflow-hidden transition-all group-hover:shadow-md">
    <div className="aspect-video bg-muted">
      <img src="..." alt="..." className="object-cover w-full h-full" />
    </div>
    <div className="p-4">
      {/* Contenido */}
    </div>
  </div>
</Link>
\`\`\`

## 🔧 Herramientas y Extensiones Recomendadas

- **Tailwind CSS IntelliSense** - Autocompletado de clases
- **Prettier** - Formateo de código
- **ESLint** - Linting de código
- **Headless UI** - Componentes accesibles (ya incluido en shadcn/ui)

---

**Nota**: Esta guía debe actualizarse cada vez que se agreguen nuevos patrones o se modifiquen los existentes. Mantener la consistencia es clave para un desarrollo escalable.