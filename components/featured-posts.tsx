import Link from "next/link"
import { ArrowRight } from "lucide-react"
import { Button } from "@/components/ui/button"

export function FeaturedPosts() {
  return (
    <section className="mt-16">
      <div className="flex items-center justify-between mb-8">
        <h2 className="text-3xl font-bold">Featured Posts</h2>
        <Button variant="ghost" className="gap-1">
          View all <ArrowRight className="h-4 w-4" />
        </Button>
      </div>
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <div className="relative rounded-xl overflow-hidden group">
          <div className="aspect-[16/9] bg-muted">
            <img
              src="/placeholder.svg?height=400&width=800&text=Featured+Post"
              alt="Featured post"
              className="object-cover w-full h-full transition-transform group-hover:scale-105 duration-300"
            />
          </div>
          <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/50 to-transparent flex flex-col justify-end p-6 text-white">
            <div className="mb-2">
              <span className="text-xs font-medium px-2 py-1 rounded-full bg-primary text-primary-foreground">
                UI/UX
              </span>
              <span className="text-xs ml-2">10 min read</span>
            </div>
            <h3 className="text-2xl font-bold mb-2 group-hover:text-primary transition-colors">
              10 UI/UX Principles Every Developer Should Know
            </h3>
            <p className="text-sm text-gray-200 mb-4 max-w-lg">
              Learn the essential UI/UX principles that will help you create better user interfaces and improve the user
              experience of your applications.
            </p>
            <div className="flex items-center gap-2">
              <div className="w-10 h-10 rounded-full bg-muted overflow-hidden">
                <img
                  src="/placeholder.svg?height=40&width=40&text=JD"
                  alt="Author"
                  className="w-full h-full object-cover"
                />
              </div>
              <div>
                <p className="font-medium">John Doe</p>
                <p className="text-xs">Mar 15, 2024</p>
              </div>
            </div>
          </div>
        </div>
        <div className="grid grid-cols-1 gap-6">
          {[1, 2, 3].map((post) => (
            <Link key={post} href={`/post/${post}`} className="group flex gap-4">
              <div className="w-24 h-24 sm:w-32 sm:h-32 rounded-lg overflow-hidden flex-shrink-0 bg-muted">
                <img
                  src={`/placeholder.svg?height=128&width=128&text=Post+${post}`}
                  alt={`Post ${post}`}
                  className="object-cover w-full h-full transition-transform group-hover:scale-105 duration-300"
                />
              </div>
              <div className="flex flex-col justify-center">
                <div className="flex items-center gap-2 mb-2">
                  <span className="text-xs font-medium px-2 py-1 rounded-full bg-primary/10 text-primary">
                    {post === 1 ? "Java" : post === 2 ? ".NET" : "Spring Boot"}
                  </span>
                  <span className="text-xs text-muted-foreground">4 min read</span>
                </div>
                <h3 className="font-semibold text-lg mb-1 group-hover:text-primary transition-colors">
                  {post === 1
                    ? "Java 21 Features You Should Start Using Today"
                    : post === 2
                      ? "Building Microservices with .NET 8"
                      : "Spring Boot 3 Best Practices"}
                </h3>
                <p className="text-muted-foreground text-sm line-clamp-2">
                  {post === 1
                    ? "Explore the latest features in Java 21 and how they can improve your code quality and productivity."
                    : post === 2
                      ? "Learn how to build scalable microservices architecture using .NET 8 and the latest tools."
                      : "Discover the best practices for building robust applications with Spring Boot 3."}
                </p>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </section>
  )
}

