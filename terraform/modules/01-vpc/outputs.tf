output "vpc_id" {
  description = "The ID of the VPC"
  value       = try(aws_vpc.vpc.id, null)
}
output "vpc_cidr_block" {
  description = "The CIDR block of the VPC"
  value       = try(aws_vpc.vpc.cidr_block, null)
}

output "vpc_subnet_ids" {
  description = "IDs of created subnets"
  value       = { for k, v in aws_subnet.this : k => v.id }
}

output "public_subnet_ids" {
  description = "IDs of public subnets"
  value       = [for k, v in aws_subnet.this : v.id if v.tags["Type"] == "public"]
}

output "private_subnet_ids" {
  description = "IDs of private subnets"
  value       = [for k, v in aws_subnet.this : v.id if v.tags["Type"] == "private"]
}