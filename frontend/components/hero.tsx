import { Button } from "@/components/ui/button"
import { HeroImage } from "./hero-image"

export function Hero() {

  return (
    <section className="py-12 md:py-24 bg-muted/50">
      <div className="container mx-auto px-4 grid md:grid-cols-2 gap-8 items-center">
        <div>
          <h1 className="text-4xl md:text-5xl font-bold tracking-tight mb-4">
            Where <span className="text-primary">developers</span> share their knowledge
          </h1>
          <p className="text-xl text-muted-foreground mb-8">
            Discover articles, tutorials, and insights from the developer community on UI/UX, Java, .NET, Spring Boot,
            and more.
          </p>
          <div className="flex flex-col sm:flex-row gap-4">
            <Button size="lg">Start reading</Button>
            <Button variant="outline" size="lg">
              Create account
            </Button>
          </div>
        </div>
        <div className="relative">
          <div className="aspect-[4/3] rounded-lg overflow-hidden">
            <HeroImage />
          </div>
          <div className="absolute -bottom-6 -left-6 bg-background p-4 rounded-lg shadow-lg border max-w-[200px]">
            <p className="font-medium mb-1">Join our community</p>
            <p className="text-sm text-muted-foreground">Connect with 10,000+ developers worldwide</p>
          </div>
        </div>
      </div>
    </section>
  )
}

