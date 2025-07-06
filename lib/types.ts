type Post = {
    id: string
    title: string
    author: string
    content: string
    category: string
    createdAt: string
    tags: string[]
    comments: Comment[]
    image: URL
}

type CreateUserRequest = {
    name: string
    username: string
    email: string
    password: string
}