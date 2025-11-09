output "vpc_id" {
  description = "The ID of the VPC"
  value       = try(module.vpc.vpc_id, null)
}

output "vpc_cidr_block" {
  description = "The CIDR block of the VPC"
  value       = try(module.vpc.vpc_cidr_block, null)
}

output "public_subnet_ids" {
  description = "IDs of created public subnets"
  value       = try(module.vpc.public_subnet_ids, null)
}

output "private_subnet_ids" {
  description = "IDs of created private subnets"
  value       = try(module.vpc.private_subnet_ids, null)
}

output "ec2_id" {
  description = "EC2 module output"
  value       = try(module.ec2, null)
}

output "alb_dns_name" {
  description = "The DNS name of the load balancer"
  value       = try(module.alb.lb_dns_name, null)
}