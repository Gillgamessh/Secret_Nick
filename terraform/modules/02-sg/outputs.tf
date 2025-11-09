################################################################################
# SG
################################################################################

output "alb_security_group_id" {
  description = "ID of the ALB security group"
  value       = try(aws_security_group.alb.id, null)
}

output "web_backend_security_group_id" {
  description = "ID of the Web Backend security group"
  value       = try(aws_security_group.web_backend.id, null)
}

output "web_ui_security_group_id" {
  description = "ID of the Web UI security group"
  value       = try(aws_security_group.web_ui.id, null)
}

output "rds_instance_security_group_id" {
  description = "ID of the RDS instance security group"
  value       = try(aws_security_group.rds.id, null)
}

output "security_groups" {
  description = "Map of all security groups"
  value = {
    alb          = aws_security_group.alb.id
    web_backend  = aws_security_group.web_backend.id
    web_ui       = aws_security_group.web_ui.id
    rds_instance = aws_security_group.rds.id
  }
}
output "rds_security_group_id" {
  description = "ID of the RDS security group"
  value       = aws_security_group.rds.id
}