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

def clearBoard(board):
  for i in range(10):
    for j in range(10):
      board[i, j] = "~"
  return board

def fillShip(ship, shipSize,  playerName):
  ret = Point[shipSize]
  done = False
  while(not done):
    start = input("Enter start point of "+ playerName + "'s " + ship + "(Length = " + shipSize + "):")
    end = input("Enter end point of " + playerName + "'s " + ship + "(Length = " + shipSize + "):")
    startArr = start.Split(',')
    endArr = end.Split(',')
    if not (len(start) == 3) or not (len(end) == 3):
      print("Sorry that ship was invalid, please try again")
      continue
    try:
      startArr1 = int(startArr[0])
      startArr2 = int(startArr[1])
      if (startArr1 < 0 or startArr1 > 9 or startArr2 < 0 or startArr2 > 9):
        print("Sorry that ship was invalid, please try again")
        continue
      endArr1 = int(endArr[0])
      endArr2 = int(endArr[1])
      if (endArr1 < 0 or endArr1 > 9 or endArr2 < 0 or endArr2 > 9):
        print("Sorry that ship was invalid, please try again")
        continue

    except:
      print("Sorry that ship was invalid, please try again")
      continue
    if (abs(startArr1 - endArr1) == 0 and abs(startArr2 - endArr2) == shipSize-1):
      if (startArr2 > endArr2):
        for i in range(endArr2, startArr2+1):
          ret[i - endArr2] = Point(startArr1, i)
        return ret
    elif abs(startArr1 - endArr1) == shipSize-1 and abs(startArr2 - endArr2) == 0:
      if (startArr1 > endArr1):
        for i in range(endArr1, startArr1 + 1):
          ret[i - endArr1] = Point(i, startArr2)
        return ret
      for i in range (startArr1, endArr1 + 1):
        ret[i - startArr1] = Point(i, startArr2)
      return ret
    print("Sorry that ship was invalid, please try again")
  return ret

def putPointsOnGrid(grid, points, name):
  for point in points:
    grid[point.X, point.Y] = name
  return grid




def main():
    print("hello")

main()