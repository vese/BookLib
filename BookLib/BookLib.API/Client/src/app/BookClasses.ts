export class ListBook {
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

export class Book {
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
  comments:
    {
      text: string,
      mark: number
    }[]
  freeCount: number
}

export class Param {
  id: number
  name: string
}

export class FilterParams {
  releaseYears: number[]
  authors: Param[]
  publishers: Param[]
  series: Param[]
  categories: { category: Param, genres: Param[] }[]
  sortProperties: { name: string, value: string }[]
}

export class ViewBook {
  name: string
  isbn: string
  description: string
  releaseYear: number
  authorId: number
  author: string
  publisherId: number
  publisher: string
  hasSeries: boolean
  seriesId: number
  series: string
  categoryId: number
  category: string
  genreId: number
  genre: string
}
