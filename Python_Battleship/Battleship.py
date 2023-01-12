class Point:
  x = 0
  y = 0
  def __init__(self, x, y):
    self.x = x
    self.y = y

class Ship:
  name = ""
  points = []
  health = 0
  def __init__(self, points, name):
    self.points = points
    self.name = name
    self.health = len(points)

class Player:
  board = [10, 10]
  guessBoard = [10, 10]

  carrier = Ship(Point[5], "carrier")
  battleship = Ship(Point[4], "battleship")
  destroyer = Ship(Point[3], "destroyer")
  submarine = Ship(Point[3], "submarine")
  patrolBoat = Ship(Point[2], "patrol boat")

  shipsLeft = 5
  def __init__(self, name):
    self.name = name


def main():
    print("hello")

main()