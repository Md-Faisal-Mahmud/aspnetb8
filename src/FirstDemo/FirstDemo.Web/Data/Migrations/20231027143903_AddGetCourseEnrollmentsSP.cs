﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstDemo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGetCourseEnrollmentsSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetCourseEnrollments]
						@PageIndex int,
						@PageSize int , 
						@OrderBy nvarchar(50),
						@CourseName nvarchar(250) = '%',
						@StudentName nvarchar(250) = '%',
						@EnrollmentDateFrom datetime = null,
						@EnrollmentDateTo datetime = null,
						@Total int output,
						@TotalDisplay int output

						AS
						BEGIN
							Declare @sql nvarchar(2000);
							Declare @countsql nvarchar(2000);
							Declare @paramList nvarchar(MAX); 
							Declare @countparamList nvarchar(MAX);
	
							SET NOCOUNT ON;

							Select @Total = count(*) from CourseStudent;
							SET @countsql = 'select @TotalDisplay = count(*) from CourseStudent cs inner join 
											Courses c on cs.CourseId = c.Id inner join
											Students s on cs.StudentId = s.Id  where 1 = 1 ';
	
							IF @CourseName IS NOT NULL
							SET @countsql = @countsql + ' AND c.Name LIKE ''%'' + @xCourseName + ''%''' 

							IF @StudentName IS NOT NULL
							SET @countsql = @countsql + ' AND s.Name LIKE ''%'' + @xStudentName + ''%''' 

							IF @EnrollmentDateFrom IS NOT NULL
							SET @countsql = @countsql + ' AND EnrollDate >= @xEnrollmentDateFrom'

							IF @EnrollmentDateTo IS NOT NULL
							SET @countsql = @countsql + ' AND EnrollDate <= @xEnrollmentDateTo' 


							SET @sql = 'select c.Name as CourseName, s.Name as StudentName, EnrollDate from CourseStudent cs inner join 
										Courses c on cs.CourseId = c.Id inner join
										Students s on cs.StudentId = s.Id where 1 = 1 '; 

							IF @CourseName IS NOT NULL
							SET @sql = @sql + ' AND c.Name LIKE ''%'' + @xCourseName + ''%''' 

							IF @StudentName IS NOT NULL
							SET @sql = @sql + ' AND s.Name LIKE ''%'' + @xStudentName + ''%''' 

							IF @EnrollmentDateFrom IS NOT NULL
							SET @sql = @sql + ' AND EnrollDate >= @xEnrollmentDateFrom'

							IF @EnrollmentDateTo IS NOT NULL
							SET @sql = @sql + ' AND EnrollDate <= @xEnrollmentDateTo' 

							SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
							ROWS FETCH NEXT @PageSize ROWS ONLY';

							SELECT @countparamlist = '@xCourseName nvarchar(250),
								@xStudentName nvarchar(250),
								@xEnrollmentDateFrom datetime,
								@xEnrollmentDateTo datetime,
								@TotalDisplay int output' ;

							exec sp_executesql @countsql , @countparamlist ,
								@CourseName,
								@StudentName,
								@EnrollmentDateFrom,
								@EnrollmentDateTo,
								@TotalDisplay = @TotalDisplay output;

							SELECT @paramlist = '@xCourseName nvarchar(250),
								@xStudentName nvarchar(250),
								@xEnrollmentDateFrom datetime,
								@xEnrollmentDateTo datetime,
								@PageIndex int,
								@PageSize int';

							exec sp_executesql @sql , @paramlist ,
								@CourseName,
								@StudentName,
								@EnrollmentDateFrom,
								@EnrollmentDateTo,
								@PageIndex,
								@PageSize;

							print @countsql;
							print @sql;
						END
                        GO";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"DROP PROCEDURE [dbo].[GetAdmissionTestApplicantList]";
            migrationBuilder.Sql(sql);
        }
    }
}
