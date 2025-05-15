select v.VID, v.VENDOR_NAME, v.VENDOR_EMAIL, v.[ADDRESS], v.VENDOR_PHONE
from VENDOR v join GAME g
on v.VID = g.VID left outer join RENTALS r
on g.GID = r.GID
and r.RENT_DATE >= DATEADD(MONTH, -1, CONVERT(DATE, GETDATE()))
group by v.VID, v.VENDOR_NAME, v.VENDOR_EMAIL, v.[ADDRESS], v.VENDOR_PHONE
having COUNT(r.GID) = 0;
