select g.GID, g.GAME_NAME
from GAME g left outer join RENTALS r
on g.GID = r.GID
and r.RENT_DATE >= DATEADD(MONTH, -1, CONVERT(DATE, GETDATE()))
group by g.GID, g.GAME_NAME
having count(r.GID) = 0