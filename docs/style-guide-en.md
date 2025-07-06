
# DevBlog – Style & Development Guide

## 🎨 Color Palette

### Primary Colors
```css
/* Light Mode */
--primary: 220 14% 11%;           /* Bluish black for main text */
--primary-foreground: 210 40% 98%; /* White for text on primary */
--secondary: 210 40% 96%;         /* Very light gray for secondary backgrounds */
--secondary-foreground: 222.2 84% 4.9%; /* Black for text on secondary */

/* Dark Mode */
--primary: 210 40% 98%;           /* White for main text */
--primary-foreground: 222.2 84% 4.9%; /* Black for text on primary */
--secondary: 217.2 32.6% 17.5%;  /* Dark gray for secondary backgrounds */
--secondary-foreground: 210 40% 98%; /* White for text on secondary */
```

### Category Colors
```css
/* UI/UX - Blue */
.category-ui-ux {
  @apply bg-blue-100 text-blue-700 dark:bg-blue-900/30 dark:text-blue-400;
}

/* Java - Orange */
.category-java {
  @apply bg-orange-100 text-orange-700 dark:bg-orange-900/30 dark:text-orange-400;
}

/* .NET - Purple */
.category-dotnet {
  @apply bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400;
}

/* Spring Boot - Green */
.category-spring {
  @apply bg-green-100 text-green-700 dark:bg-green-900/30 dark:text-green-400;
}

/* Databases - Red */
.category-databases {
  @apply bg-red-100 text-red-700 dark:bg-red-900/30 dark:text-red-400;
}
```

## 📝 Typography

### Title Hierarchy
```css
/* Main Title (Hero) */
.title-hero {
  @apply text-4xl md:text-5xl font-bold tracking-tight;
}

/* Section Title */
.title-section {
  @apply text-3xl font-bold;
}

/* Article Title */
.title-article {
  @apply text-2xl font-bold;
}

/* Card Title */
.title-card {
  @apply text-lg font-semibold;
}

/* Subtitle */
.subtitle {
  @apply text-xl text-muted-foreground;
}
```

### Body Text
```css
/* Main text */
.text-body {
  @apply text-base leading-relaxed;
}

/* Small text */
.text-small {
  @apply text-sm;
}

/* Extra small text */
.text-xs-custom {
  @apply text-xs;
}

/* Muted text */
.text-muted {
  @apply text-muted-foreground;
}
```

## 📏 Spacing

### Spacing System
```css
/* Spacing between main sections */
.section-spacing {
  @apply mt-16;
}

/* Spacing between elements */
.element-spacing {
  @apply mb-8;
}

/* Small spacing */
.small-spacing {
  @apply mb-4;
}

/* Extra small spacing */
.xs-spacing {
  @apply mb-2;
}
```

### Containers
```css
/* Main container */
.container-main {
  @apply container mx-auto px-4;
}

/* Content container */
.container-content {
  @apply max-w-3xl mx-auto px-4;
}

/* Wide container */
.container-wide {
  @apply max-w-6xl mx-auto px-4;
}
```

## 🎯 Reusable Components

### Buttons
```css
/* Primary button */
.btn-primary {
  @apply bg-primary text-primary-foreground hover:bg-primary/90;
}

/* Secondary button */
.btn-secondary {
  @apply bg-secondary text-secondary-foreground hover:bg-secondary/80;
}

/* Outline button */
.btn-outline {
  @apply border border-input bg-background hover:bg-accent hover:text-accent-foreground;
}

/* Ghost button */
.btn-ghost {
  @apply hover:bg-accent hover:text-accent-foreground;
}
```

### Cards
```css
/* Base card */
.card-base {
  @apply border rounded-lg overflow-hidden transition-all hover:shadow-md;
}

/* Post card */
.card-post {
  @apply border rounded-lg overflow-hidden transition-all group-hover:shadow-md;
}

/* Category card */
.card-category {
  @apply border rounded-lg p-6 transition-all hover:shadow-md group;
}
```

### Badges/Tags
```css
/* Base badge */
.badge-base {
  @apply text-xs font-medium px-2 py-1 rounded-full;
}

/* Category badge */
.badge-category {
  @apply text-xs font-medium px-2 py-1 rounded-full bg-primary/10 text-primary;
}
```

## 📱 Responsive Design

### Breakpoints
```css
/* Mobile First Approach */
/* xs: 0px - 639px (phones) */
/* sm: 640px - 767px (large phones) */
/* md: 768px - 1023px (tablets) */
/* lg: 1024px - 1279px (laptops) */
/* xl: 1280px - 1535px (desktops) */
/* 2xl: 1536px+ (large screens) */
```

### Responsive Grids
```css
/* Posts grid */
.grid-posts {
  @apply grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6;
}

/* Categories grid */
.grid-categories {
  @apply grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-4;
}

/* Featured posts grid */
.grid-featured {
  @apply grid grid-cols-1 lg:grid-cols-2 gap-8;
}
```

## 🎭 Animations and Transitions

### Standard Transitions
```css
/* Basic transition */
.transition-base {
  @apply transition-all duration-200 ease-in-out;
}

/* Hover transition */
.transition-hover {
  @apply transition-colors duration-200;
}

/* Transform transition */
.transition-transform {
  @apply transition-transform duration-300 ease-in-out;
}
```

### Hover Effects
```css
/* Hover on cards */
.hover-card {
  @apply hover:shadow-md transition-all;
}

/* Hover on images */
.hover-image {
  @apply transition-transform group-hover:scale-105 duration-300;
}

/* Hover on text */
.hover-text {
  @apply hover:text-primary transition-colors;
}
```

## 🖼️ Images and Media

### Image Aspects
```css
/* Hero image */
.aspect-hero {
  @apply aspect-[4/3];
}

/* Post image */
.aspect-post {
  @apply aspect-video;
}

/* Square image */
.aspect-square {
  @apply aspect-square;
}

/* Avatar */
.avatar-size {
  @apply w-8 h-8 rounded-full;
}
```

### Placeholders
```css
/* For placeholder images, always use: */
/placeholder.svg?height={height}&width={width}&text={text}
```

## 🎨 Dark Mode

### Implementation
```css
/* Always use dark: classes for dark mode */
.dark-compatible {
  @apply bg-background text-foreground;
  @apply dark:bg-background dark:text-foreground;
}

/* For background elements */
.dark-bg {
  @apply bg-muted/50 dark:bg-muted/50;
}
```

## 🚀 Performance

### Best Practices

1. **Images**
   - Always use descriptive `alt`
   - Implement lazy loading with `loading="lazy"`
   - Use a placeholder while loading

2. **CSS**
   - Prefer Tailwind classes over custom CSS
   - Avoid `!important`
   - Use CSS variables for repeated values

3. **JavaScript**
   - Use `"use client"` only when necessary
   - Default to Server Components
   - Lazy load heavy components

## 📋 Development Checklist

### Before creating a component:
- [ ] Is it reusable?
- [ ] Does it follow the color palette?
- [ ] Is it responsive?
- [ ] Does it support dark mode?
- [ ] Does it have hover/focus states?
- [ ] Is it accessible?

### Before committing:
- [ ] Consistent colors
- [ ] Proper spacing
- [ ] Fully responsive across breakpoints
- [ ] Dark mode works
- [ ] Optimized for performance
- [ ] Accessibility implemented

## 🎯 Design Patterns

### Main Layout
```tsx
<div className="min-h-screen flex flex-col">
  <Header />
  <main className="flex-1">
    {/* Content */}
  </main>
  <Footer />
</div>
```

### Standard Section
```tsx
<section className="mt-16">
  <div className="container mx-auto px-4">
    <h2 className="text-3xl font-bold mb-8">Title</h2>
    {/* Content */}
  </div>
</section>
```

### Post Card
```tsx
<Link href="/post/slug" className="group">
  <div className="border rounded-lg overflow-hidden transition-all group-hover:shadow-md">
    <div className="aspect-video bg-muted">
      <img src="..." alt="..." className="object-cover w-full h-full" />
    </div>
    <div className="p-4">
      {/* Content */}
    </div>
  </div>
</Link>
```

## 🔧 Recommended Tools & Extensions

- **Tailwind CSS IntelliSense** – Class autocompletion
- **Prettier** – Code formatting
- **ESLint** – Code linting
- **Headless UI** – Accessible components (already included in shadcn/ui)

---

**Note**: This guide should be updated whenever new patterns are added or existing ones are modified. Maintaining consistency is key to scalable development.
