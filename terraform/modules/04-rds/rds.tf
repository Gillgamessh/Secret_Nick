################################################################################
# Private subnet group for the DB instance
################################################################################
resource "aws_db_subnet_group" "private" {
  name       = var.db_subnet_group_name
  subnet_ids = var.vpc_subnet_ids
}
################################################################################
# DB instance
################################################################################
resource "aws_db_instance" "db" {
  identifier        = var.db_id
  port              = var.rds_port
  engine            = var.db_engine
  engine_version    = var.db_engine_version
  instance_class    = var.db_instance_class
  allocated_storage = var.db_storage_size
  storage_type      = "gp2"

  username                    = var.db_username
  manage_master_user_password = true
  vpc_security_group_ids      = var.db_vpc_sg_ids

  db_subnet_group_name = aws_db_subnet_group.private.name
  publicly_accessible  = false
  multi_az             = false
  skip_final_snapshot  = true


  # Additional recommended settings
  # backup_retention_period = 7
  # backup_window           = "03:00-04:00"
  # maintenance_window      = "mon:04:00-mon:05:00"

  storage_encrypted = true

  tags = { "Name" = var.db_id }

}