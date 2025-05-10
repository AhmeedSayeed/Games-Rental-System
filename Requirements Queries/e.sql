select v.VID, v.VENDOR_NAME, v.VENDOR_EMAIL, v.[ADDRESS], v.VENDOR_PHONE
from VENDOR v join GAME g
on v.VID = g.VID left outer join RENTALS r
on g.GID = r.GID
group by v.VID, v.VENDOR_NAME, v.VENDOR_EMAIL, v.[ADDRESS], v.VENDOR_PHONE
having COUNT(r.RENT_DATE) = 0;
