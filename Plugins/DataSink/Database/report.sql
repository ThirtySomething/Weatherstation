SELECT mv.id,
       mt.NAME, 
       mv.VALUE, 
       mu.NAME, 
       mv.recordtime 
FROM   mvalue mv 
       LEFT JOIN munit mu 
              ON mv.unitid = mu.id 
       LEFT JOIN mtype mt 
              ON mv.typeid = mt.id 
ORDER  BY mv.typeid, 
          mv.id;
