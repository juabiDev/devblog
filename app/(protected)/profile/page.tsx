import { Footer } from "@/components/footer"
import { Header } from "@/components/header"

export default function ProfilePage() {
    return (
        <>
            <div className="min-h-screen flex flex-col">
                <Header />
                <main className="flex-1 flex items-center justify-center py-12">
                    <div className="w-full max-w-md px-4">
                        <div className="text-center mb-8">
                            <h1 className="text-3xl font-bold">Profile</h1>
                        </div>
                    </div>
                </main>
                <Footer />
            </div>
        </>
    )
}