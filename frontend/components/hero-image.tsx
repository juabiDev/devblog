"use client"

import { useTheme } from "next-themes";
import { useEffect, useState } from "react";
    
export const HeroImage = () => {
    const { theme } = useTheme();
    const [mounted, setMounted] = useState(false);
  
    useEffect(() => {
      setMounted(true); 
    }, []);
  
    if (!mounted) return null;
  
    const isDark = theme === "dark";
  
    return (
      <img
        src={isDark ? "/devblog-hero-dark.png" : "/devblog-hero.png"}
        alt="DevBlog Hero"
        className="object-cover w-full h-full"
        height={500}
        width={600}
      />
    );
  };