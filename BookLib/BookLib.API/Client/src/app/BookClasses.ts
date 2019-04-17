export class ListBook {
  id: number
  name: string
  author: string
  publisher: string
  croppedDescription: string
  category: string
  genre: string
  series: string
  commentsCount: number
  averageMark: number
  releaseYear: number
  freeCount: number
}

export class BooksList {
  books: ListBook[]
  count: number
}

export class BookComment {
  text: string
  mark: number
  name: string
}

export class Book {
  name: string
  author: string
  publisher: string
  description: string
  category: string
  genre: string
  series: string
  commentsCount: number
  averageMark: number
  releaseYear: number
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
  authorId: number
  author: string
  publisherId: number
  publisher: string
  description: string
  releaseYear: number
  hasSeries: boolean
  seriesId: number
  series: string
  categoryId: number
  category: string
  genreId: number
  genre: string
}
