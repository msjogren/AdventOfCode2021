//target area: x=32..65, y=-225..-177
var targetArea = (x: (from: 32, to: 65), y: (from: -225, to: -177));

(int highest, int velocityCount) TraceTrajectory()
{
    int totalHighest = int.MinValue;
    int foundVelocityCount = 0;

    for (int velocityX = 1; velocityX <= 200; velocityX++)
    {
        for (int velocityY = -1000; velocityY < 1000; velocityY++)
        {
            int vX = velocityX, vY = velocityY;
            var currentPos = (x: 0, y: 0);
            int highestY = currentPos.y;

            while (currentPos.x <= targetArea.x.to)
            {
                currentPos.x += vX;
                currentPos.y += vY;
                vX -= Math.Sign(vX);
                vY -= 1;
                
                if (currentPos.x > targetArea.x.to || currentPos.y < targetArea.y.from) break;

                if (currentPos.y > highestY)
                {
                    highestY = currentPos.y;
                }

                if (currentPos.x >= targetArea.x.from && currentPos.x <= targetArea.x.to &&
                    currentPos.y >= targetArea.y.from && currentPos.y <= targetArea.y.to)
                {
                    foundVelocityCount++;
                        
                    if (highestY > totalHighest)
                    {
                        totalHighest = highestY;
                    } 

                    break;
                }
            }
        }
    }

    return (totalHighest, foundVelocityCount);
}

var (part1, part2) = TraceTrajectory();
Console.WriteLine(part1);
Console.WriteLine(part2);
