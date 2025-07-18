import Link from "next/link"
import { Button } from "@/components/ui/button"
import { Header } from "@/components/header"
import { Hero } from "@/components/hero"
import { FeaturedPosts } from "@/components/featured-posts"
import { Categories } from "@/components/categories"
import { Footer } from "@/components/footer"
import PostList from "@/components/post"

export default function Home() {

  return (
    
    <div className="min-h-screen flex flex-col">
      <Header />
      <main className="flex-1">
        <Hero />
        <div className="container mx-auto px-4 py-8">
          <FeaturedPosts />
          <Categories />
          <section className="mt-16">
            <div className="flex items-center justify-between mb-8">
              <h2 className="text-3xl font-bold">Latest Posts</h2>
              <Button variant="outline">View All</Button>
            </div>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              <PostList />
            </div>
          </section>
        </div>
      </main>
      <Footer />
    </div>
  )
}

