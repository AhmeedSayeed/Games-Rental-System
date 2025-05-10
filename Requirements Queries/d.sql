select top 1 v.VID, v.VENDOR_NAME, v.VENDOR_EMAIL, v.[ADDRESS], v.VENDOR_PHONE, COUNT(r.RENT_DATE) as "Number of Rents"
from VENDOR v, GAME g, RENTALS r
where v.VID = g.VID
	and g.GID = r.GID
	and r.RENT_DATE >= DATEADD(MONTH, -1, GETDATE())
group by v.VID, v.VENDOR_NAME, v.VENDOR_EMAIL, v.[ADDRESS], v.VENDOR_PHONE
order by COUNT(r.RENT_DATE)
