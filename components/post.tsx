import { calculateTimeRead, postDataFormatter } from "@/lib/utils"
import Link from "next/link"

async function getPosts() {
    const res = await fetch('http://localhost:5000/api/posts', {
      // Para evitar caché si estás en desarrollo:
      cache: 'no-store',
      headers: {
        'Content-Type': 'application/json'
      },
      method: 'GET'
    })
  
    if (!res.ok) {
      throw new Error('Error al cargar los posts')
    }
  
    return res.json()
}
  
export default async function PostList() {
    const posts = await getPosts()
    const formattedPosts = posts.map(postDataFormatter)
  
    return (
        <>
        {formattedPosts.map((post: Post) => {
            const formattedDate = new Date(post.createdAt).toLocaleString('es-ES', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
            });

            return (
                <Link key={post.id} href={`/post/${post.id}`} className="group">
                <div className="border rounded-lg overflow-hidden transition-all group-hover:shadow-md">
                <div className="aspect-video bg-muted relative">
                    <img
                    src={post.image.toString()}
                    alt={`Post ${post.title}`}
                    className="object-cover w-full h-full"
                    />
                </div>
                <div className="p-4">
                    <div className="flex items-center gap-2 mb-2">
                    <span className="text-xs font-medium px-2 py-1 rounded-full bg-primary/10 text-primary">
                        {post.category}
                    </span>
                    <span className="text-xs text-muted-foreground">{calculateTimeRead(post.content)} min read</span>
                    </div>
                    <h3 className="font-semibold text-lg mb-2 group-hover:text-primary transition-colors">
                        {post.title}
                    </h3>
                    <p className="text-muted-foreground text-sm line-clamp-2">
                        {post.content}
                    </p>
                    <div className="flex items-center gap-2 mt-4">
                    <div className="w-8 h-8 rounded-full bg-muted overflow-hidden">
                        <img
                        src={`/placeholder.svg?height=32&width=32&text=A${post.author}`}
                        alt="Author"
                        className="w-full h-full object-cover"
                        />
                    </div>
                    <div>
                        <p className="text-sm font-medium">Author {post.author}</p>
                        <p className="text-xs text-muted-foreground">Published {formattedDate}</p>
                    </div>
                    </div>
                </div>
                </div>
            </Link>
            )
            
        })}
        </>
    )
  }