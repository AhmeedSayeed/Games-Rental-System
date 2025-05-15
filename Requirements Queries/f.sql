-- Who were the vendors who didn't add any game last year?
SELECT V.VID, V.VENDOR_NAME
FROM VENDOR V
LEFT JOIN GAME G
    ON V.VID = G.VID AND YEAR(G.RELEASE_DATE) = YEAR(GETDATE()) - 1
WHERE G.GID IS NULL;
