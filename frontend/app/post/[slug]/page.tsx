import { Header } from "@/components/header"
import { Footer } from "@/components/footer"
import { Post } from "@/components/post"

export default async function PostPage({ params }: { params: { slug: string } }) {

  return (
    <div className="min-h-screen flex flex-col">
      <Header />
      <main className="flex-1">
        <Post 
          postID={params.slug}
        />
      </main>
      <Footer />
    </div>
  )
}

