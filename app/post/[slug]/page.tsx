import Link from "next/link"
import { ArrowLeft, MessageSquare, ThumbsUp, Share2, Bookmark } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Header } from "@/components/header"
import { Footer } from "@/components/footer"

export default async function PostPage({ params }: { params: { slug: string } }) {

  await new Promise((resolve) => setTimeout(resolve, 3000)); // Simula retraso

  return (
    <div className="min-h-screen flex flex-col">
      <Header />
      <main className="flex-1">
        <article className="max-w-3xl mx-auto px-4 py-8">
          <div className="mb-8">
            <Link href="/" className="inline-flex items-center text-sm font-medium text-primary mb-4">
              <ArrowLeft className="mr-2 h-4 w-4" />
              Back to home
            </Link>
            <h1 className="text-3xl md:text-4xl font-bold mb-4">
              Building Modern Web Applications with Next.js and TypeScript
            </h1>
            <div className="flex items-center gap-4 mb-6">
              <div className="flex items-center gap-2">
                <Avatar>
                  <AvatarImage src="/placeholder.svg?height=40&width=40&text=JD" alt="John Doe" />
                  <AvatarFallback>JD</AvatarFallback>
                </Avatar>
                <div>
                  <p className="text-sm font-medium">John Doe</p>
                  <p className="text-xs text-muted-foreground">Mar 15, 2024 · 8 min read</p>
                </div>
              </div>
              <div className="flex items-center gap-2">
                <span className="text-xs font-medium px-2 py-1 rounded-full bg-primary/10 text-primary">
                  Next.js
                </span>
                <span className="text-xs font-medium px-2 py-1 rounded-full bg-primary/10 text-primary">
                  TypeScript
                </span>
              </div>
            </div>
            <div className="aspect-[21/9] rounded-lg overflow-hidden mb-8">
              <img
                src="/placeholder.svg?height=600&width=1200&text=Next.js+TypeScript"
                alt="Next.js and TypeScript"
                className="object-cover w-full h-full"
              />
            </div>
          </div>
          <div className="prose prose-lg dark:prose-invert max-w-none">

          </div>
          <div className="border-t border-b py-6 my-8">
            <div className="flex justify-between items-center">
              <div className="flex items-center gap-4">
                <Button variant="ghost" size="sm" className="gap-2">
                  <ThumbsUp className="h-4 w-4" />
                  <span>124</span>
                </Button>
                <Button variant="ghost" size="sm" className="gap-2">
                  <MessageSquare className="h-4 w-4" />
                  <span>23</span>
                </Button>
              </div>
              <div className="flex items-center gap-2">
                <Button variant="ghost" size="icon">
                  <Share2 className="h-4 w-4" />
                </Button>
                <Button variant="ghost" size="icon">
                  <Bookmark className="h-4 w-4" />
                </Button>
              </div>
            </div>
          </div>
          <div className="border-b pb-8">
            <h3 className="text-lg font-semibold mb-4">About the author</h3>
            <div className="flex gap-4">
              <Avatar className="h-12 w-12">
                <AvatarImage src="/placeholder.svg?height=48&width=48&text=JD" alt="John Doe" />
                <AvatarFallback>JD</AvatarFallback>
              </Avatar>
              <div>
                <p className="font-medium">John Doe</p>
                <p className="text-sm text-muted-foreground mb-2">
                  Frontend Developer specializing in React and Next.js
                </p>
                <Button variant="outline" size="sm">
                  Follow
                </Button>
              </div>
            </div>
          </div>
        </article>
      </main>
      <Footer />
    </div>
  )
}

