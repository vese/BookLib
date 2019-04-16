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
  position: number
}

export class LibBook {
  id: number
  name: string
  free: number
  onHands: number
  queueLength: number
}
