-- Who was the renter (client) with the maximum renting last month?

SELECT u.UID , u.USER_NAME , u.USER_EMAIL , u.USER_PHONE , COUNT(*) MAXIMUM_RENTING
FROM [USER] u , RENTALS r
WHERE u.UID = r.UID

-- for the previous month
-- AND r.RENT_DATE >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)
-- AND r.RENT_DATE < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)

-- for the last 30 days
AND r.RENT_DATE >= DATEADD(DAY , -30 , GETDATE())
AND r.RENT_DATE < GETDATE()

GROUP BY u.UID , u.USER_NAME , u.USER_EMAIL , u.USER_PHONE
HAVING COUNT(*) = (
	SELECT MAX(cnt)
	FROM (
		SELECT COUNT(*) cnt
		FROM RENTALS
		WHERE RENT_DATE >= DATEADD(DAY , -30 , GETDATE())
		AND RENT_DATE < GETDATE()
		GROUP BY UID
	) counts
)