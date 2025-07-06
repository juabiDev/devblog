"use client"

import type React from "react"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Checkbox } from "@/components/ui/checkbox"
import { Header } from "@/components/header"
import { Footer } from "@/components/footer"
import { signUp } from "@/app/api/api"

export default function SignUpPage() {
  const [rememberMe, setRememberMe] = useState(false)

  // TODO: add validations
  const handleValidation = (data : any) => {
    return {validate: true, errors: ""}
  }

  const handleSubmit = async (e : React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const form = e.currentTarget;
    const formData = Object.fromEntries(new FormData(form).entries());

    const validation = handleValidation(formData);

    // TODO: add error messages below form
    if(!validation.validate) {
        return;
    } 

    const formatterData : CreateUserRequest = {
        name: formData.name.toString(), 
        username: formData.username.toString(), 
        email: formData.email.toString(), 
        password: formData.password.toString()
    }

    const isSignedUp = await signUp(formatterData);

    // TODO: redirection to home and dialog message

  }

  return (
    <div className="min-h-screen flex flex-col">
      <Header />
      <main className="flex-1 flex items-center justify-center py-12">
        <div className="w-full max-w-md px-4">
          <div className="text-center mb-8">
            <h1 className="text-3xl font-bold">Sign Up</h1>
            <p className="text-muted-foreground mt-2">Enter your information to create your account</p>
          </div>
          <div className="space-y-4">
            <form onSubmit={handleSubmit} className="space-y-4">
              <div className="space-y-2">
                <Label htmlFor="name">Name</Label>
                <Input
                  id="name"
                  type="text"
                  name="name"
                  placeholder="name"
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="username">UserName</Label>
                <Input
                  id="username"
                  type="text"
                  name="username"
                  placeholder="username"
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="email">Email</Label>
                <Input
                  id="email"
                  type="email"
                  name="email"
                  placeholder="m@example.com"
                  required
                />
              </div>
              <div className="space-y-2">
                <div className="flex items-center justify-between">
                  <Label htmlFor="password">Password</Label>
                </div>
                <Input
                  id="password"
                  type="password"
                  name="password"
                  placeholder=".................."
                  required
                />
              </div>
              <div className="space-y-2">
                <div className="flex items-center justify-between">
                  <Label htmlFor="repeatedpassword">Repeat Password</Label>
                </div>
                <Input
                  id="repeatedpassword"
                  type="password"
                  name="password"
                  placeholder=".................."
                  required
                />
              </div>

              <Button type="submit" className="w-full">
                Sign Up
              </Button>
            </form>
          </div>
        </div>
      </main>
      <Footer />
    </div>
  )
}

