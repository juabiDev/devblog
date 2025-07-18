import { calculateTimeRead, postDataFormatter } from "@/lib/utils"
import { AvatarFallback, AvatarImage } from "@radix-ui/react-avatar"
import { UUID } from "crypto"
import Link from "next/link"
import { Avatar } from '@/components/ui/avatar';
import { ArrowLeft, Bookmark, MessageSquare, Share2, ThumbsUp } from "lucide-react";
import { Button } from "./ui/button";
import { getPostById, getPosts } from "@/app/api/api";

export async function Post({ postID } : any) {
    const post = await getPostById(postID)
    const postFormatted = postDataFormatter(post);

    console.log(post)
    
    const formattedDate = new Date(post.createdAt).toLocaleString('es-ES', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
    });

    return (
        <article className="max-w-3xl mx-auto px-4 py-8">
          <div className="mb-8">
            <Link href="/" className="inline-flex items-center text-sm font-medium text-primary mb-4">
              <ArrowLeft className="mr-2 h-4 w-4" />
              Back to home
            </Link>
            <h1 className="text-3xl md:text-4xl font-bold mb-4">
              {postFormatted.title}
            </h1>
            <div className="flex items-center gap-4 mb-6">
              <div className="flex items-center gap-2">
                <Avatar>
                  <AvatarImage src="/placeholder.svg?height=40&width=40&text=JD" alt={postFormatted.author} />
                  <AvatarFallback>{postFormatted.author}</AvatarFallback>
                </Avatar>
                <div>
                  <p className="text-sm font-medium">{postFormatted.author}</p>
                  <p className="text-xs text-muted-foreground">{formattedDate} · {calculateTimeRead(postFormatted.content)} min read</p>
                </div>
              </div>
              {postFormatted.tags.length > 0 &&  
                <div className="flex items-center gap-2">
                    {postFormatted.tags.map(tag => (
                        <span className="text-xs font-medium px-2 py-1 rounded-full bg-primary/10 text-primary">
                        {tag}
                        </span>
                    ))}
                </div>
              }

            </div>
            <div className="aspect-[21/9] rounded-lg overflow-hidden mb-8">
              {
              postFormatted.image == null || postFormatted.image.toString().length == 0 
              ? 
                <img
                    src="/placeholder.svg?height=600&width=1200&text=Next.js+TypeScript"
                    alt="Next.js and TypeScript"
                    className="object-cover w-full h-full"
                />
              :
                <img
                    src={postFormatted.image.toString()}
                    alt={postFormatted.title}
                    className="object-cover w-full h-full"
                />
            }
            </div>
          </div>
          <div className="prose prose-lg dark:prose-invert max-w-none">
            <p className="text-lg text-gray-700 leading-relaxed mt-6">
                {postFormatted.content}
            </p>
          </div>
          <div className="border-t border-b py-6 my-8">
            <div className="flex justify-between items-center">
              <div className="flex items-center gap-4">
                <Button variant="ghost" size="sm" className="gap-2">
                  <ThumbsUp className="h-4 w-4" />
                  <span>0</span>
                </Button>
                <Button variant="ghost" size="sm" className="gap-2">
                  <MessageSquare className="h-4 w-4" />
                  <span>{postFormatted.comments.length}</span>
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
    )
}
  
export default async function PostList() {
    const posts = await getPosts()
    const formattedPosts = posts != null && posts.length > 0 ? posts.map(postDataFormatter) : []
  
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