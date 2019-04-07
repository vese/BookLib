export const BOOKS: ListBook[] =
  [
    {
      id: 1,
      name: "q",
      croppedDescription: "q",
      releaseYear: 1,
      author: "q",
      publisher: "q",
      category: "q",
      genre: "q",
      series: "q",
      commentsCount: 1,
      averageMark: 1,
      freeCount: 1,
    },
    {
      id: 2,
      name: "w",
      croppedDescription: "w",
      releaseYear: 2,
      author: "w",
      publisher: "w",
      category: "w",
      genre: "w",
      series: "w",
      commentsCount: 2,
      averageMark: 2,
      freeCount: 2,
    }
  ]

export class ListBook
{
  id: number
  name: string
  croppedDescription: string
  releaseYear: number
  author: string
  publisher: string
  category: string
  genre: string
  series: string
  commentsCount: number
  averageMark: number
  freeCount: number
}

export class Comment
{
  text: string
  mark: number
}

export class Book
{
  name: string
  isbn: string
  description: string
  releaseYear: number
  author: string
  publisher: string
  category: string
  genre: string
  series: string
  commentsCount: number
  averageMark: number
  comments: Comment[]
  freeCount: number
}
