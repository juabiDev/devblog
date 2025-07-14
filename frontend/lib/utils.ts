import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function postDataFormatter(post: any) : Post {
    const imageUrl = post.imageUrl ? new URL(post.imageUrl) : null

    return {
      id: post.id,
      title: post.title,
      author: post.authorName,
      content: post.content,
      category: post.categoryName,
      createdAt: post.createdAt,
      tags: post.tags,
      comments: post.comments,
      image: post.imageUrl,
    }
}

export function calculateTimeRead(text: string, wordsPerMinute = 200) {
  const words = text.trim().split(/\s+/).length;
  const minutes = words / wordsPerMinute;
  const time = Math.ceil(minutes);
  return time;
}