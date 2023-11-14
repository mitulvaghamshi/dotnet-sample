--(1) GROUP - 1, SELECT - A
PRINT 'GROUP - 1, SELECT - A';
SELECT COUNT(*) 'child patients (age < 18)'
FROM patients
WHERE FLOOR(DATEDIFF(DAY, birth_date, GETDATE()) / 365.25) < 18;

--(2) GROUP - 1, SELECT - C
PRINT 'GROUP - 1, SELECT - C';
SELECT first_name, last_name, patient_height
FROM patients
WHERE gender = 'F' AND patient_height >= (
  SELECT MAX(patient_height)
  FROM patients
  WHERE gender = 'F'
);

--(3) GROUP - 2, SELECT - C
PRINT 'GROUP - 2, SELECT - C';
SELECT province_id, COUNT(*) 'patients'
FROM patients
WHERE province_id NOT IN ('ON')
GROUP BY province_id;

--(4) GROUP - 2, SELECT - D
PRINT 'GROUP - 2, SELECT - D';
SELECT gender, COUNT(*) 'patients'
FROM patients
WHERE patient_height > 175
GROUP BY gender;

--(5) GROUP - 3, SELECT - A
PRINT 'GROUP - 3, SELECT - A';
SELECT p.patient_id, p.first_name, p.last_name, a.room, a.bed
FROM patients p
JOIN admissions a ON p.patient_id = a.patient_id
WHERE a.nursing_unit_id IN (
  SELECT nursing_unit_id
  FROM nursing_units
  WHERE nursing_unit_id = '2SOUTH'
) AND a.discharge_date IS NULL
ORDER BY p.last_name;

--(6) GROUP - 3, SELECT - D
PRINT 'GROUP - 3, SELECT - D';
SELECT d.department_id, d.department_name, d.manager_first_name,
 d.manager_last_name, p.purchase_order_id, p.total_amount
FROM departments d
JOIN purchase_orders p ON d.department_id = p.department_id
WHERE p.total_amount >= 1500
ORDER BY d.department_id;

--(7) GROUP - 4, SELECT - A
PRINT 'GROUP - 4, SELECT - A';
SELECT h.physician_id, h.first_name, h.last_name, h.specialty
FROM physicians h
JOIN encounters e ON h.physician_id = e.physician_id
JOIN patients p ON e.patient_id = p.patient_id
WHERE p.first_name = 'Harry' AND p.last_name = 'Sullivan';

--(8) GROUP - 4, SELECT - B
PRINT 'GROUP - 4, SELECT - B';
SELECT p.patient_id, p.first_name, p.last_name,
 a.nursing_unit_id, a.primary_diagnosis
FROM patients p
JOIN admissions a ON p.patient_id = a.patient_id
JOIN physicians h ON a.attending_physician_id = h.physician_id
WHERE a.discharge_date IS NULL AND h.specialty = 'Internist';

--(9) GROUP - 5, SELECT - B
PRINT 'GROUP - 5, SELECT - B';
SELECT p.first_name + ' ' + p.last_name 'patient',
 a.nursing_unit_id, a.room, m.medication_description
FROM unit_dose_orders o
JOIN admissions a ON o.patient_id = a.patient_id
JOIN patients p ON o.patient_id = p.patient_id
JOIN medications mON o.medication_id= m.medication_id
WHERE a.discharge_date IS NULL AND p.allergies = 'Penicillin';

--(10) GROUP - 6, SELECT - B
PRINT 'GROUP - 6, SELECT - B';
SELECT o.purchase_order_id, o.order_date, o.department_id
FROM purchase_orders o
WHERE NOT EXISTS (
  SELECT l.purchase_order_id
  FROM purchase_order_lines l
  WHERE l.purchase_order_id = o.purchase_order_id
);
