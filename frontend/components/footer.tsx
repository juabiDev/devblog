import Link from "next/link"
import { Github, Twitter, Linkedin, Mail } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"

export function Footer() {
  return (
    <footer className="border-t mt-16 py-12 bg-muted/30">
      <div className="container mx-auto px-4">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          <div className="md:col-span-2">
            <Link href="/" className="text-2xl font-bold text-primary">
              DevBlog
            </Link>
            <p className="mt-4 text-muted-foreground max-w-md">
              A community for developers to share knowledge, insights, and experiences across various programming
              languages and frameworks.
            </p>
            <div className="flex space-x-4 mt-6">
              <Button variant="ghost" size="icon" asChild>
                <Link href="https://github.com">
                  <Github className="h-5 w-5" />
                  <span className="sr-only">GitHub</span>
                </Link>
              </Button>
              <Button variant="ghost" size="icon" asChild>
                <Link href="https://twitter.com">
                  <Twitter className="h-5 w-5" />
                  <span className="sr-only">Twitter</span>
                </Link>
              </Button>
              <Button variant="ghost" size="icon" asChild>
                <Link href="https://linkedin.com">
                  <Linkedin className="h-5 w-5" />
                  <span className="sr-only">LinkedIn</span>
                </Link>
              </Button>
              <Button variant="ghost" size="icon" asChild>
                <Link href="mailto:info@devblog.com">
                  <Mail className="h-5 w-5" />
                  <span className="sr-only">Email</span>
                </Link>
              </Button>
            </div>
          </div>
          <div>
            <h3 className="font-semibold mb-4">Categories</h3>
            <ul className="space-y-2">
              <li>
                <Link
                  href="/categories/ui-ux"
                  className="text-sm text-muted-foreground hover:text-primary transition-colors"
                >
                  UI/UX
                </Link>
              </li>
              <li>
                <Link
                  href="/categories/java"
                  className="text-sm text-muted-foreground hover:text-primary transition-colors"
                >
                  Java
                </Link>
              </li>
              <li>
                <Link
                  href="/categories/dotnet"
                  className="text-sm text-muted-foreground hover:text-primary transition-colors"
                >
                  .NET
                </Link>
              </li>
              <li>
                <Link
                  href="/categories/spring"
                  className="text-sm text-muted-foreground hover:text-primary transition-colors"
                >
                  Spring Boot
                </Link>
              </li>
              <li>
                <Link
                  href="/categories/databases"
                  className="text-sm text-muted-foreground hover:text-primary transition-colors"
                >
                  Databases
                </Link>
              </li>
            </ul>
          </div>
          <div>
            <h3 className="font-semibold mb-4">Subscribe to our newsletter</h3>
            <p className="text-sm text-muted-foreground mb-4">
              Get the latest articles and resources sent to your inbox weekly.
            </p>
            <div className="flex gap-2">
              <Input placeholder="Enter your email" type="email" />
              <Button>Subscribe</Button>
            </div>
          </div>
        </div>
        <div className="border-t mt-12 pt-6 flex flex-col md:flex-row justify-between items-center">
          <p className="text-sm text-muted-foreground">© {new Date().getFullYear()} DevBlog. All rights reserved.</p>
          <div className="flex gap-4 mt-4 md:mt-0">
            <Link href="/terms" className="text-sm text-muted-foreground hover:text-primary transition-colors">
              Terms
            </Link>
            <Link href="/privacy" className="text-sm text-muted-foreground hover:text-primary transition-colors">
              Privacy
            </Link>
            <Link href="/cookies" className="text-sm text-muted-foreground hover:text-primary transition-colors">
              Cookies
            </Link>
          </div>
        </div>
      </div>
    </footer>
  )
}

