import { Param } from './bookclasses';

export class LibUser {
  name: string
  onHands: number
  returned: number
  expired: number
  notReturned: number
}

export class QueueOnBook {
  id: number
  name: string
  author: string
  position: number
  available: boolean
}

export class LibBook {
  id: number
  name: string
  free: number
  onHands: number
  queueLength: number
}

export class Notifications {
  queue: Param[]
  onHands: {
    name: string
    days: number
    notificationLevel: number
  }[]
}

export class InQueue {
  inQueue: boolean
  position: number
}

export class BookOnHands {
  id: number
  name: string
  author: string
  getDate: Date
  returnDate: Date
}
