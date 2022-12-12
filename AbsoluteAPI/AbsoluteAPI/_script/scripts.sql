SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

-------------------------------------
-------------------------------------
ALTER VIEW APP_ELENCO_SQUADRE_V AS
	SELECT 
		DISTINCT(EVENTI_SQUADRE.ID_SQUADRA),	
		SQUADRE.NOME,
		SQUADRE.LOGO
	FROM 
		EVENTI_SQUADRE
	JOIN 
		SQUADRE
ON 
	EVENTI_SQUADRE.ID_SQUADRA = SQUADRE.ID
	WHERE EVENTI_SQUADRE.STATO = 1
	AND EVENTI_SQUADRE.ID_EVENTO IN (SELECT ID_EVENTI FROM APP_LS_COMPETIZIONI_EVENTI_V)
	--ORDER BY NOME ASC



-------------------------------------
-------------------------------------
CREATE PROCEDURE [MSSql27442].[Sp_ProssimeGare] @time nvarchar(4), @idsquadra int
AS
SELECT * FROM APP_INCONTRI_V 
WHERE (
		(
			(CONVERT(date, DATA_EVENTO,112) = CONVERT(date, GETDATE(),112) AND (REPLACE(ORA, ':', '') > @time))
		OR 
			(CONVERT(date, DATA_EVENTO,112) > CONVERT(date, GETDATE(),112))
		)  
		AND 
			(ID_SQUADRA_1 =@idsquadra OR ID_SQUADRA_2=@idsquadra) 
		)
		OR 
		(
			(
				(CONVERT(date, DATA_POSTICIPO,112) = CONVERT(date, GETDATE(),112) AND (REPLACE(ORA_POSTICIPO, ':', '') > @time))
			OR (CONVERT(date, DATA_POSTICIPO,112) > CONVERT(date, GETDATE(),112))
			)
			AND (ID_SQUADRA_1 = @idsquadra OR ID_SQUADRA_2=@idsquadra)
		) 
			
			
			
ORDER BY isnull(DATA_POSTICIPO, data_Evento) ASC, isnull(ORA_POSTICIPO, ORA) ASC, DATA_EVENTO ASC, ORA ASC
GO


-------------------------------------
-------------------------------------


