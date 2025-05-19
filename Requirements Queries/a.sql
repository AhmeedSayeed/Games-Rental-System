select top 1 with ties g.GID, g.GAME_NAME, count(*) as "Number of rentals"
from GAME g, RENTALS r
where g.GID = r.GID
group by g.GID, g.GAME_NAME
order by count(*) desc