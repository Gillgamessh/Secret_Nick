variable "vpc_name" {
  description = "VPC name"
  type        = string
}

variable "vpc_cidr" {
  description = "Your VPC adress block"
  type        = string
}

variable "subnets" {
  description = "List of subnets"
  type = map(object({
    cidr_block = string
    public     = bool
    az_index   = optional(number)
  }))
}

variable "tags" {
  description = "A map of tags to add to all resources"
  type        = map(string)
  default     = {}
}
