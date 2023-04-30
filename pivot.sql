

select '' Subject,[1],[2],[3],[4],[5] from tbl_exam_setup es
unpivot ([Value] FOR Type in ([exam_type])) Unp
pivot (Max([Value]) FOR e_id IN ([1],[2],[3],[4],[5])) Piv
union all
select subject_name,
'',
'',
'',
'',
''
from tbl_subject

select '' Subject,'FA1' '1','FA2' '2','FA3' '3','FA4' '4','FA5' '5'
union all
select subject_name,'','','','','' from tbl_subject