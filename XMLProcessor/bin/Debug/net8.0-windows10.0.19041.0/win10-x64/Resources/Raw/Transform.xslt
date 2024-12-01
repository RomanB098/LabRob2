<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html" indent="yes"/>

	<xsl:template match="/">
		<html>
			<head>
				<title>Search Results</title>
				<style>
					table {
					width: 100%;
					border-collapse: collapse;
					}
					th, td {
					border: 1px solid black;
					padding: 8px;
					text-align: left;
					}
					th {
					background-color: #f2f2f2;
					}
				</style>
			</head>
			<body>
				<h1>Search Results</h1>
				<xsl:choose>
					<xsl:when test="/Scientists/Scientist">
						<table>
							<tr>
								<th>Faculty</th>
								<th>Department</th>
								<th>Full Name</th>
								<th>Position</th>
								<th>Years on Position</th>
								<th>Salary</th>
							</tr>
							<xsl:for-each select="/Scientists/Scientist">
								<tr>
									<td>
										<xsl:value-of select="@Faculty"/>
									</td>
									<td>
										<xsl:value-of select="@Department"/>
									</td>
									<td>
										<xsl:value-of select="concat(@FirstName, ' ', @MiddleName, ' ', @LastName)"/>
									</td>
									<td>
										<xsl:value-of select="@Position"/>
									</td>
									<td>
										<xsl:value-of select="@YearsOnPosition"/>
									</td>
									<td>
										<xsl:value-of select="@Salary"/>
									</td>
								</tr>
							</xsl:for-each>
						</table>
					</xsl:when>
					<xsl:otherwise>
						<p>No results found.</p>
					</xsl:otherwise>
				</xsl:choose>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
