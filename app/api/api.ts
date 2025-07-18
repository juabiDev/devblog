export async function login(email: string, password: string): Promise<boolean> {
    try {
      const res = await fetch("http://localhost:5000/auth/login", {
      method: "POST",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password }),
    });

    console.log(res);
    return res.ok;
  } catch (error) {
    return false;
  }
}

export async function logout(): Promise<boolean> {
    try {
      const res = await fetch("http://localhost:5000/auth/logout", {
        method: "POST",
        credentials: "include",
      });

      return res.ok;
    } catch (error) {
        return false;
    }
}

export async function signUp(form : CreateUserRequest) : Promise<boolean> {
  try {
    const res = await fetch("http://localhost:5000/api/users", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(form),
    });

    return res.ok;
  } catch (error) {
    return false;
  }
}

export async function getPosts() : Promise<Post[] | null> {
  try {
    const res = await fetch('http://localhost:5000/api/posts', {
      cache: 'no-store',
      headers: {
        'Content-Type': 'application/json'
      },
      method: 'GET'
    })
  
    if (res.ok) {
      return res.json()
    } else {
      return [];
    }
  } catch(error) {
    return null;
  }

}

export async function getPostById(postID : any): Promise<Post | null> {
  try {
    const res = await fetch(`http://localhost:5000/api/posts/${postID}`, {
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
  } catch(error) {
    return null;
  }
}