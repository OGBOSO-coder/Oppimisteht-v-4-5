UPDATE orders
SET carrier_id = 1,
    tracking_number = 'DH123456789',
    shipped_date = NOW(),
    order_status = 'Shipped'
WHERE order_id = 1;

UPDATE orders
SET delivered_date = NOW(),
    order_status = 'Delivered'
WHERE order_id = 1;