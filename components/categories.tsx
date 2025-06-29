import Link from "next/link"
import { Code, Layers, Layout, Database, Server } from "lucide-react"

export function Categories() {
  const categories = [
    {
      name: "UI/UX",
      description: "User interface and experience design",
      icon: <Layout className="h-6 w-6" />,
      color: "bg-blue-100 text-blue-700 dark:bg-blue-900/30 dark:text-blue-400",
      href: "/categories/ui-ux",
    },
    {
      name: "Java",
      description: "Java programming and ecosystem",
      icon: <Code className="h-6 w-6" />,
      color: "bg-orange-100 text-orange-700 dark:bg-orange-900/30 dark:text-orange-400",
      href: "/categories/java",
    },
    {
      name: ".NET",
      description: ".NET framework and C# development",
      icon: <Layers className="h-6 w-6" />,
      color: "bg-purple-100 text-purple-700 dark:bg-purple-900/30 dark:text-purple-400",
      href: "/categories/dotnet",
    },
    {
      name: "Spring Boot",
      description: "Spring Boot framework and applications",
      icon: <Server className="h-6 w-6" />,
      color: "bg-green-100 text-green-700 dark:bg-green-900/30 dark:text-green-400",
      href: "/categories/spring",
    },
    {
      name: "Databases",
      description: "Database design and optimization",
      icon: <Database className="h-6 w-6" />,
      color: "bg-red-100 text-red-700 dark:bg-red-900/30 dark:text-red-400",
      href: "/categories/databases",
    },
  ]

  return (
    <section className="mt-16">
      <h2 className="text-3xl font-bold mb-8">Popular Categories</h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-4">
        {categories.map((category) => (
          <Link
            key={category.name}
            href={category.href}
            className="border rounded-lg p-6 transition-all hover:shadow-md group"
          >
            <div className={`${category.color} w-12 h-12 rounded-lg flex items-center justify-center mb-4`}>
              {category.icon}
            </div>
            <h3 className="font-semibold text-lg mb-1 group-hover:text-primary transition-colors">{category.name}</h3>
            <p className="text-sm text-muted-foreground">{category.description}</p>
          </Link>
        ))}
      </div>
    </section>
  )
}

