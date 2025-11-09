################################################################################
# RDS
################################################################################
output "db_instance_arn" {
  description = "The ARN of the RDS instance"
  value       = aws_db_instance.db.arn
}

output "db_instance_id" {
  description = "The RDS instance identifier"
  value       = aws_db_instance.db.id
}

output "db_instance_status" {
  description = "The current status of the RDS instance"
  value       = aws_db_instance.db.status
}

output "db_endpoint" {
  description = "The connection endpoint for the database"
  value       = aws_db_instance.db.endpoint
}

output "db_port" {
  description = "The port the database is listening on"
  value       = aws_db_instance.db.port
}